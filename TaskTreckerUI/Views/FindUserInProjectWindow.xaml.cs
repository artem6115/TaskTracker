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

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для FindUserInProjectWindow.xaml
    /// </summary>
    public partial class FindUserInProjectWindow : Window
    {
        public User User { get; set; }
        public List<User> users { get; private set; }
        public FindUserInProjectWindow(long projectId)
        {
            InitializeComponent();
            Load(projectId);
        }
        private async void Load(long projectId)
        {
            users = await ProjectService.GetUsers(projectId);
        }
        private void Text_changed(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Find_text.Text) || users is null) {
                User_list.ItemsSource = users.Select(x=>$"{x.FullName}, {x.Email}");
                return;
            }
            User_list.ItemsSource = users.Where(x=>x.Email.Contains(Find_text.Text) 
            || x.FullName.Contains(Find_text.Text)).Select(x => $"{x.FullName}, {x.Email}");
          //  User_list.ItemTemplate = new DataTemplate()
        }

        private void Close(object sender, EventArgs e) => Close();
        private void Chouse_btn(object sender, EventArgs e)
        {
            if(User_list.SelectedIndex == -1) {
                MessageBox.Show("Пользователь не выбран", "Not Find", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            User = users[User_list.SelectedIndex];
            Close();
        }

    }
}
