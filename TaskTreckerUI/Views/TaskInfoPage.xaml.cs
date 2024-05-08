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
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskInfoPage.xaml
    /// </summary>
    public partial class TaskInfoPage : Page
    {
        TaskInfoVm _context;
        Navigator _navigator;
        public  TaskInfoPage(Navigator navigator, long TaskId)
        {
            InitializeComponent();
            _navigator = navigator;
            _context = DataContext as TaskInfoVm;
            _context.TaskId = TaskId;
            LoadData();
           
        }
        private async void LoadData() {
            var result = await _context.LoadData();
            if (result)
            {
                Comment_btn.IsEnabled = true;
                if (_context.Task.Epic is null)
                    Title = $"Мои Задачи / {_context.Task?.Title}";
                else
                    Title = $"{_context.Task.Epic.Project?.Name} / {_context.Task.Epic.Title} / {_context.Task.Title}";

                _navigator.SetTitle(true);
            }
            else
                _navigator.SetTitle(false);

        }
        private void Open_back_task(object sender, MouseButtonEventArgs e)
        {
            if (_context.Task?.PreviousTask?.Id is null) return;
            _navigator.Open(new TaskInfoPage(_navigator,_context.Task.PreviousTask.Id));
        }
        private void Open_Comments(object sender, EventArgs e) 
            => _navigator.Open(new CommentPage(_navigator,_context.Task));
        

    }
}
