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
    /// Логика взаимодействия для NotifyPage.xaml
    /// </summary>
    public partial class NotifyPage : Page
    {
        Navigator _navigator;
        NotifyVm _context;
        public NotifyPage(Navigator navigator)
        {
            _navigator = navigator;
            InitializeComponent();
            _context = DataContext as NotifyVm;

        }
        private async void DeleteAll(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить все уведомления?", "Delete all", MessageBoxButton.YesNo, MessageBoxImage.Information)
                != MessageBoxResult.Yes) return;
            var result = await NotifyService.DeleteAllNotifies();
            if (result) { _navigator.AddInformation("Все уведомления удалены"); _context.Notifies = null!; }
            else _navigator.AddError("Удаление не прошло");

        }
        private async void Delete(object sender, EventArgs e)
        {
            if (NotifyList.SelectedItem is null) return;
            var notify = NotifyList.SelectedItem as Notify;
            var result = await NotifyService.DeleteNotify(notify.Id);
            if (result) {
                _context.Notifies?.Remove(_context.Notifies.Single(x=>x.Id ==notify.Id));
                _navigator.AddInformation("Все уведомления удалены"); 
                }
            else _navigator.AddError("Удаление не прошло");

        }
    }
}
