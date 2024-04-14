using System;
using System.Collections.Generic;
using System.Configuration;
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
using TaskTreckerUI.Models;
using TaskTreckerUI.Services;

namespace TaskTreckerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            TryAuthorization();
        }

        private async void TryAuthorization()
        {
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private async void Create_Account(object sender, RoutedEventArgs e)
        {
            var user = new UserDto()
            {
                Email = Auth_Email.Text.Trim(),
                FullName = Auth_FullName.Text.Trim(),
                Password = Auth_Password.Password.Trim()
            };
            if(!UserValidator.PasswordValidator(user.Password,out var errorPassword) | 
                !UserValidator.FullNameValidator(user.FullName, out var errorName) |
                !UserValidator.EmailValidator(user.Email, out var errorEmail))
            {
                Error_text.Text = $"{errorName}{errorEmail}{errorPassword}";
                Error_text.Visibility = Visibility.Visible;
                return;
            }
            Regist_btn.IsEnabled = false;
            var result = await AuthService.CreateUser(user);
            if(AuthService.User is not null)
            { 
                Close();
                return;
            }
            Error_text.Text = result.ErrorMessage;
            Error_text.Visibility = Visibility.Visible;
            Regist_btn.IsEnabled = true;


        }

        private async void Login_Button(object sender, RoutedEventArgs e)
        {
            var user = new UserDto()
            {
                Email = Login_Email.Text,
                Password = Login_Password.Password
            };
            if (!UserValidator.PasswordValidator(user.Password, out var errorPassword) |
              !UserValidator.EmailValidator(user.Email, out var errorEmail))
            {
                Error_text.Text = $"{errorEmail}{errorPassword}";
                Error_text.Visibility = Visibility.Visible;
                return;
            }
            Login_btn.IsEnabled = false;
            var result = await AuthService.Login(user);
            if (AuthService.User is not null)
            {
                Close();
                return;
            }
            Error_text.Text = result.ErrorMessage;
            Error_text.Visibility = Visibility.Visible;
            Login_btn.IsEnabled = true;
        }
    }
}
