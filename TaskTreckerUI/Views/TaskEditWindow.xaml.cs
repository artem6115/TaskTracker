using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskEditWindow.xaml
    /// </summary>
    public partial class TaskEditWindow : Window
    {
        public TaskInfo Task { get; private set; }
        TaskInfoVm _context;
        public TaskEditWindow(long? taskId = null!, List<TaskDto> tasks = null!)
        {
            InitializeComponent();
            _context = DataContext as TaskInfoVm;
            _context.TaskId = taskId;
            _context.TasksInScope = tasks;
            Init();
        }
        private async void Init() {
            if (_context.TasksInScope is null || _context.TasksInScope.Count == 0)
            {
                tasks_combo.IsEnabled = false;
                BackTask_check.IsChecked = true;
                BackTask_check.IsEnabled = false;

            }

            if (_context.TaskId is null) 
                {
                _context.Task = new TaskInfo();
                return;
                }
            var result = await _context.LoadData();
            if (!result)
            {
                MessageBox.Show("Данные задачи не удалось загрузить", "Loading error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Date_check.IsChecked = _context.Task.ApproximateDateOfCompleted is null;
            Importance_check.IsChecked = _context.Task.Importance is null;
            BackTask_check.IsChecked = _context.Task.PreviousTask is null;
            tasks_combo.SelectedValue = _context.Task.PreviousTask?.Id;


        }

        private void Exit(object sender, RoutedEventArgs e) => Close();

        private void Exec(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_context.Task.Title))
            {
                MessageBox.Show("Заголовок задачи не должен быть пустым", "Validation error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_context.Task.Description))
            {
                MessageBox.Show("Описание задачи не должен быть пустым", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task = _context.Task;
            if (tasks_combo.SelectedItem is not null)Task.PreviousTask = tasks_combo.SelectedItem as TaskDto;

            if (Date_check.IsChecked == true) Task.ApproximateDateOfCompleted = null;
            if (Importance_check.IsChecked == true) Task.Importance = null;
            if (BackTask_check.IsChecked == true) Task.PreviousTask = null;
            Task.User = AuthService.User;
            if (_context.Task.StatusTask == Models.TaskStatus.Blocked && Task.PreviousTask == null)
                Task.StatusTask = Models.TaskStatus.Work;
            Close();
          
        }
    }
}
