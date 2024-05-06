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
    /// Логика взаимодействия для PageEditWindow.xaml
    /// </summary>
    public partial class PageEditWindow : Window
    {
        public Project Model { get;private set; }
        public bool DoDelete { get; private set; } = false;

        UserFinder finder;
        ProjectEditVm _context;
        public PageEditWindow(long? projectId = null)
        {
            InitializeComponent();
            _context = DataContext as ProjectEditVm;
            _context.ProjectId = projectId;
            LoadData();
            finder = new UserFinder();
        }
        public async Task LoadData()
        {
            await _context.LoadData();
            if (_context.ProjectId != null && _context.Project == null)
                MessageBox.Show("Не удалось загрузить проект", "Loaded error", MessageBoxButton.OK, MessageBoxImage.Error);
            _context.Project ??= new Project() { Author= AuthService.User};
            
        }
        private void Close_btn(object sender, RoutedEventArgs e)
            => Close();


        private async void Open_email_helper(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Users_list.IsDropDownOpen = false;
                return;
            }
            else if (e.Key != Key.Enter) return;
            var currentText = Users_list.Text;

            var listUser = await finder.GetUsers(Users_list.Text);
            if (currentText != Users_list.Text) return;

            Users_list.ItemsSource = listUser;
            Users_list.Text = currentText;
            Users_list.IsSynchronizedWithCurrentItem = false;
            Users_list.IsDropDownOpen = true;
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            if (Users_list.SelectedItem == null) return;
            var user = Users_list.SelectedItem as User;
            if(_context.Project.Users.FirstOrDefault(x=>x.Email==user.Email) == null)
                _context.Project.Users.Add(user);
        }
        private void Delete_user(object sender, EventArgs e)
        {
            if(Users_List_To_Change.SelectedItem == null) return;
            var user = Users_List_To_Change.SelectedItem as User;
            _context.Project.Users.Remove(user);
        }
        private void Save_Data(object sender, EventArgs e)
        {
            if (!ValidateModel()) return;
            Model = _context.Project;
            Close();
        }

        private bool ValidateModel()
        {
            bool valid = true;
            var error = "";
            if (string.IsNullOrWhiteSpace(_context.Project.Name))
            {
                valid = false;
                error += "Название проекта не должно быть пустым\n";
            }
            if (_context.Project.Name?.Length >20)
            {
                valid = false;
                error += "Название проекта не должно быть больше 20 символов\n";
            }
            if (string.IsNullOrWhiteSpace(_context.Project.Description))
            {
                valid = false;
                error += "Описание проекта не должно быть пустым\n";
            }
            if (!valid)
                MessageBox.Show(error, "Error list", MessageBoxButton.OK, MessageBoxImage.Warning);
            return valid;
        }

        private void Delete_Data(object sender, EventArgs e)
        {
            if(_context.ProjectId is null )
            {
                MessageBox.Show(
                "Нельзя удалить проект который не был создан", "Delete project"
                , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show(
                "Вы точно хотите удалить проект?", "Delete project"
                , MessageBoxButton.YesNo, MessageBoxImage.Question
                )
                != MessageBoxResult.Yes)
                return;
            DoDelete = true;
            Close();
        }

        private async void Change_Author(object sender, EventArgs e)
        {
            if (Users_list.SelectedItem == null) return;
            var user = Users_list.SelectedItem as User;
            _context.Project.Author = user;
            _context.OnPropertyChanged("Project.Author.Email");
            Author_textblock.Text = $"Руководитель: {_context.Project.Author.Email}";
        }
        private async void Delete_User(object sender, EventArgs e)
        {
            if (Users_list.SelectedItem == null) return;
            var user = Users_list.SelectedItem as User;
            _context.Project.Users.Remove(_context.Project.Users.Single(x=>x.Id==user.Id));

        }

    }
}
