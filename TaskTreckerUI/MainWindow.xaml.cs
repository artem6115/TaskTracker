using System.Text;
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
using TaskTrackerUI.Views;
using static System.Net.Mime.MediaTypeNames;

namespace TaskTrackerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
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

        }



        private void Open_Notes(object sender, RoutedEventArgs e)
        {
            NavWindows.Navigate(new NotesPage());
        }

        private void Back_Page(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Page(object sender, RoutedEventArgs e)
        {

        }

        private void Reload_Page(object sender, RoutedEventArgs e)
        {

        }
    }
}