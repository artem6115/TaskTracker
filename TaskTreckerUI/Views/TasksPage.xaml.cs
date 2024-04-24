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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskTrackerUI.Filters;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        TaskFilter Filter;
        TaskVm _context;
        Navigator _navigator;
        public TasksPage(Navigator navigator)
        {
            InitializeComponent();
            _navigator = navigator;
            Filter = new TaskFilter() { TaskStatus=Models.TaskStatus.All};
            _context = DataContext as TaskVm;
            _context.Filter = Filter;
        }

        private void Select_type(object sender, SelectionChangedEventArgs e)
        {
            var chooseItem = Task_Types.SelectedItem as ComboBoxItem;
            var tag = chooseItem.Tag;
            if (tag == null) return;
            var index = int.Parse(tag.ToString());
            Filter.TaskStatus = (Models.TaskStatus)index;
            _context.Refresh();

        }

        private async void Delete_Task(object sender, RoutedEventArgs e)
        {
            var task = TaskList.SelectedItem as TaskDto;
            if (task == null) return;
            //TASK IF PROJECT SI NOT NULL THROW ERROR
            if (MessageBox.Show(
                $"Удалить задачу?\n({task.Title})","Удаление задачи"
                ,MessageBoxButton.YesNo
                ,MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                var result = await TaskService.DeleteTaskAsync(task);
                if (!result) _navigator.AddError("Не удалось удалить задачу");
                else { _navigator.AddInformation("Задача удалена");
                    var item = _context.Tasks.Single(x=>x.Id == task.Id);
                    _context.Tasks.Remove(item);
                }

            }
        }

        private void Show_task_btn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var task = TaskList.SelectedItem as TaskDto;
                if (task == null) return;
                _navigator.Open(new TaskInfoPage(task.Id));
            }
        }

        private void Show_task_mouse(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var task = TaskList.SelectedItem as TaskDto;
                if (task == null) return;
                _navigator.Open(new TaskInfoPage(task.Id));
            }
        }
    }
}
