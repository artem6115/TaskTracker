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
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        Navigator _navigator;
        SettingVm _context;
        public SettingsPage(Navigator navigator)
        {
            _navigator = navigator;
            InitializeComponent();
            _context = DataContext as SettingVm;
            period_update.SelectedValue = _context.Setting.DelaySecond;
        }

        private void Exit_Account(object sender, RoutedEventArgs e)
        {
            SettingService.Setting.Token = null!;
            SettingService.Setting.RefreshToken = null!;
            Thread.CurrentThread.Abort();

        }

        private async void Drop_Password(object sender, RoutedEventArgs e)
        {
            var result = await AuthService.RemovePassword(passwordBox.Password.Trim(),oldPasswordBox.Password.Trim());
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

        private void Change_period(object sender, SelectionChangedEventArgs e)
        {
            if (period_update.SelectedValue == null) return;
            _context.Setting.DelaySecond = int.Parse(period_update.SelectedValue.ToString());
            _navigator.UpdateDelayTimer();


        }
        private void Start_Update(object sender, EventArgs e)
            => _navigator.UpdateContinue();
        private void Stop_Update(object sender, EventArgs e)
            => _navigator.UpdateStop();

        private void Auto_Update(object sender, EventArgs e)
        {
            if (auto_update_check.IsChecked==true) _navigator.StartAutoUpdate();
            else _navigator.StopAutoUpdate();
        }
    }
}
