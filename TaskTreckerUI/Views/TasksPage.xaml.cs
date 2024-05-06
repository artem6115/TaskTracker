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
        private void Select_project(object sender, SelectionChangedEventArgs e)
        {
            if (Projects_task.SelectedItem is null) return;
            var chooseItem = Projects_task.SelectedItem as ProjectDto;
            Filter.Project = chooseItem;
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
                _navigator.Open(new TaskInfoPage(_navigator,task.Id),false);
            }
        }

        private void Show_task_mouse(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var task = TaskList.SelectedItem as TaskDto;
                if (task == null) return;
                _navigator.Open(new TaskInfoPage(_navigator, task.Id),false);
            }
        }

        private async void Set_Pause(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = (long)btn.Tag;
            var task = _context.Tasks.Single(x => x.Id == tag);
            if (task.StatusTask == Models.TaskStatus.Blocked)
            {
                MessageBox.Show("Вы не можете изменить статус заблокированой задачи!", "Status error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = await TaskService.SetStatusAsync(task.Id,Models.TaskStatus.Pause);
            if(result is not null)
            {
                _context.Tasks.Single(x => x.Id == result.Id).StatusTask = result.StatusTask;
                _navigator.AddInformation("Стутус задачи изменён");
                await _context.LoadData();

            }
            else
                _navigator.AddError("Изменить статус не удалось");


        }
        private async void Set_Continue(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = (long)btn.Tag;
            var task = _context.Tasks.Single(x => x.Id == tag);
            if (task.StatusTask == Models.TaskStatus.Blocked)
            {
                MessageBox.Show("Вы не можете изменить статус заблокированой задачи!", "Status error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = await TaskService.SetStatusAsync(task.Id, Models.TaskStatus.Work);
            if (result is not null)
            {
                _context.Tasks.Single(x => x.Id == result.Id).StatusTask = result.StatusTask;
                _navigator.AddInformation("Стутус задачи изменён");
                await _context.LoadData();


            }
            else
                _navigator.AddError("Изменить статус не удалось");

        }
        private async void Set_Completed(object sender, RoutedEventArgs e)
        {
            
            var btn = sender as Button;
            var tag = (long)btn.Tag;
            var task = _context.Tasks.Single(x => x.Id == tag);
            if(task.StatusTask == Models.TaskStatus.Blocked)
            {
                MessageBox.Show("Вы не можете изменить статус заблокированой задачи!", "Status error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show("Задача выполнена ?", "Is completed", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
            var result = await TaskService.SetStatusAsync(task.Id, Models.TaskStatus.Completed);
            if (result is not null)
            {
                _context.Tasks.Single(x => x.Id == result.Id).StatusTask = result.StatusTask;
                _navigator.AddInformation("Стутус задачи изменён");
                await _context.LoadData();

            }
            else
                _navigator.AddError("Изменить статус не удалось");

        }

        private async void Create_Task(object sender, RoutedEventArgs e)
        {
            var taskForm = new TaskEditWindow(tasks: _context.TasksView.ToList());
            taskForm.ShowDialog();
            if (taskForm.Task is null) return;
            var result =await TaskService.CreateTaskAsync(taskForm.Task);
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
            var taskForm = new TaskEditWindow(taskSelected.Id,tasks);
            taskForm.ShowDialog();
            if (taskForm.Task is null) return;

            var result = await TaskService.UpdateTaskAsync(taskForm.Task);
            if (result is not null)
            {
                var entityToUpdate = _context.Tasks.Single(x=>x.Id == result.Id);
                var index = _context.Tasks.IndexOf(entityToUpdate);
                _context.Tasks.RemoveAt(index);

                _context.Tasks.Insert(index,result);
                _context.Refresh();
                _navigator.AddInformation("Задача изменена");
            }
            else
                _navigator.AddError("Изменить задачу не удалось");

        }
        private async void LocalOnly_btn(object sender, EventArgs e)
        {
            Filter.LocalOnly = LocalOnly_checkbox.IsChecked==true;
            EditTask_menu.Visibility = LocalOnly_checkbox.IsChecked == true? Visibility.Visible : Visibility.Collapsed;
            _context.Refresh();

        }
        private async void ProjectOnly_btn(object sender, EventArgs e)
        {
            Filter.Project = null;
            Projects_task.SelectedItem=null;
            Task_Types.SelectedIndex = Task_Types.Items.Count - 1;
            _context.Refresh();

        }

    }
}
