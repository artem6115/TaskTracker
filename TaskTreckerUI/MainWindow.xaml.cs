﻿using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TaskTrackerUI.Models;
using TaskTrackerUI.Properties;
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;
using TaskTrackerUI.Views;
using static System.Net.Mime.MediaTypeNames;

namespace TaskTrackerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Page[] Pages = new Page[Enum.GetNames(typeof(PagesEnum)).Length];
        Stack<Page> BackPage = new Stack<Page>();
        Stack<Page> NextPage = new Stack<Page>();
        Page CurrentPage;
        MainWindowVM _dataContext;
        Navigator Navigator;

        public MainWindow() 
        {
            Services.SettingService.Setting = Models.Setting.LoadSettings();
            InitializeComponent();
            Navigator = new Navigator(NavWindows,Page_Title,new DispatcherTimer());

            _dataContext = DataContext as MainWindowVM;
            var connection = new ConnectionWindow();
            connection.ShowDialog();
            if (LocalConnectionService.Adress is null)
            {
                Close();
                return;
            }
            if (AuthService.User is null)
            {
                var auth = new AuthWindow();
                auth.ShowDialog();
                if (AuthService.User is null) Close();
            }
            _dataContext.User = AuthService.User;
            StartupPages();

        }

        private void StartupPages()
        {
            Pages[(int)PagesEnum.Main] = new MainPage();
            Pages[(int)PagesEnum.Notify] = new NotifiesPage();
            Pages[(int)PagesEnum.Project] = new ProjectsPage(Navigator);
            Pages[(int)PagesEnum.Task] = new TasksPage(Navigator);
            Pages[(int)PagesEnum.Note] = new NotesPage(Navigator);
            Pages[(int)PagesEnum.Setting] = new SettingsPage(Navigator);
            Navigator.Open(Pages[(int)PagesEnum.Main]);

        }


        private void Back_Page(object sender, RoutedEventArgs e)
            => Navigator.Back();

        private void Next_Page(object sender, RoutedEventArgs e)
            => Navigator.Next();

        private async void Reload_Page(object sender, RoutedEventArgs e)
            => await Navigator.LoadData();

        private void Main_Open(object sender, RoutedEventArgs e) => OpenPage(PagesEnum.Main);

        private void Notify_Open(object sender, RoutedEventArgs e) => OpenPage(PagesEnum.Notify);

        private void Note_Open(object sender, RoutedEventArgs e) => OpenPage(PagesEnum.Note);

        private void Task_Open(object sender, RoutedEventArgs e) => OpenPage(PagesEnum.Task);
        private void Project_Open(object sender, RoutedEventArgs e) => OpenPage(PagesEnum.Project);

        private void Setting_Open(object sender, RoutedEventArgs e) => OpenPage(PagesEnum.Setting);
        private void OpenPage(PagesEnum pageName)
            => Navigator.Open(Pages[(int)pageName]);

        private async void Shortcut(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemMinus) Navigator.Back();
            else if (e.Key == Key.OemPlus) Navigator.Next();
            else if (e.Key == Key.Escape)
            {
                if (MessageBox.Show("Завершить работу с программой?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    Close();
            }
            else if (e.Key == Key.F1) OpenPage(PagesEnum.Main);
            else if (e.Key == Key.F6) OpenPage(PagesEnum.Notify);
            else if (e.Key == Key.F4) OpenPage(PagesEnum.Note);
            else if (e.Key == Key.F3) OpenPage(PagesEnum.Task);
            else if (e.Key == Key.F5) await Navigator.LoadData();
            else if (e.Key == Key.F2) OpenPage(PagesEnum.Project);
            else if (e.Key == Key.F7) OpenPage(PagesEnum.Setting);

        }

    }
}