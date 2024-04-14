using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.ViewModels
{
    public class MainWindowVM : VMBase
    {
        User _user;

        public MainWindowVM()
        {
            User = AuthService.User;
        }
        public User User { get => _user; set { _user = value; OnPropertyChanged(); } }
        ObservableCollection<Page> _pages;
        Page? _currentWindow = null;
        public ObservableCollection<Page> Pages {
            get => _pages; set {
                _pages = value;
                OnPropertyChanged();
            } }
        public Page CurrentWindow
        {
            get => _currentWindow; set
            {
                _currentWindow = value;
                OnPropertyChanged();
            }
        }
        

        public async override Task<bool> LoadData() => true;
    }
}
