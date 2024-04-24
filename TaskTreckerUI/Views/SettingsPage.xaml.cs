using System;
using System.Collections.Generic;
using System.IO;
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

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        Navigator _navigator;
        public SettingsPage(Navigator navigator)
        {
            _navigator = navigator;
            InitializeComponent();
        }

        private void Exit_Account(object sender, RoutedEventArgs e)
        {
            File.Delete(AuthService.TokenPath);
            File.Delete(AuthService.RefreshTokenPath);
            Thread.CurrentThread.Abort();

        }

        private async void Drop_Password(object sender, RoutedEventArgs e)
        {
            var result = await AuthService.RemovePassword(passwordBox.Text.Trim(),oldPasswordBox.Text.Trim());
            if (result.Item1) { 
                _navigator.AddInformation("Пароль успешно обновлен");
                error_label.Text = "";
             }
            else
            {
                error_label.Text = result.Item2;
                _navigator.AddError("Пароль не удалось обновить");
            }
        }
    }
}
