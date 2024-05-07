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
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для EpicTasksPage.xaml
    /// </summary>
    public partial class EpicTasksPage : Page
    {
        Navigator _navigator;
        TaskVm _context;
        public EpicTasksPage(Epic epic,Navigator navigator,bool AccesseDebind = false)
        {
            InitializeComponent();
            if(AccesseDebind) Menu_bar.Visibility = Visibility.Hidden;
            _navigator = navigator;
            Title = $"Проекты / {epic.Project.Name} / {epic.Title}";
            _context = DataContext as TaskVm;
            _context.EpicId = epic.Id;
            _context.Epic = epic;
            
        }


        private void Show_task_btn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var task = TaskList.SelectedItem as TaskDto;
                if (task == null) return;
                _navigator.Open(new TaskInfoPage(_navigator, task.Id), false);
            }
        }

        private void Show_task_mouse(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var task = TaskList.SelectedItem as TaskDto;
                if (task == null) return;
                _navigator.Open(new TaskInfoPage(_navigator, task.Id), false);
            }
        }
        private async void Create_Task(object sender, RoutedEventArgs e)
        {
            var taskForm = new TaskEditWindow(projectId: _context.Epic?.Project.Id,tasks: _context.TasksView.ToList());
            taskForm.ShowDialog();
            var task = taskForm.Task;
            if (task is null) return;
            task.EpicId = _context.EpicId;
            var result = await TaskService.CreateTaskAsync(task);
            if (result is not null)
            {
                _context.Tasks.Add(result);
                _navigator.AddInformation("Задача добавлена");
                _context.Refresh();

            }
            else
                _navigator.AddError("Добавить задачу не удалось");
        }
        private async void Edit_Task(object sender, RoutedEventArgs e)
        {
            var taskSelected = TaskList.SelectedItem as TaskDto;
            if (taskSelected == null) return;
            var tasks = _context.TasksView.ToList();
            tasks.Remove(taskSelected);
            var taskForm = new TaskEditWindow(taskSelected.Id,_context.Epic.Project.Id ,tasks);
            taskForm.ShowDialog();
            var task = taskForm.Task;
            if (task is null) return;
            var result = await TaskService.UpdateTaskAsync(taskForm.Task);
            if (result is not null)
            {
                var entityToUpdate = _context.Tasks.Single(x => x.Id == result.Id);
                var index = _context.Tasks.IndexOf(entityToUpdate);
                _context.Tasks.RemoveAt(index);

                _context.Tasks.Insert(index, result);
                _context.Refresh();
                _navigator.AddInformation("Задача изменена");
            }
            else
                _navigator.AddError("Изменить задачу не удалось");

        }
        private async void Delete_Task(object sender, RoutedEventArgs e)
        {
            var task = TaskList.SelectedItem as TaskDto;
            if (task == null) return;
            if (MessageBox.Show(
                $"Удалить задачу?\n({task.Title})", "Удаление задачи"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                var result = await TaskService.DeleteTaskAsync(task);
                if (!result) _navigator.AddError("Не удалось удалить задачу");
                else
                {
                    _navigator.AddInformation("Задача удалена");
                    var item = _context.Tasks.Single(x => x.Id == task.Id);
                    _context.Tasks.Remove(item);
                }

            }
        }

    }
}
