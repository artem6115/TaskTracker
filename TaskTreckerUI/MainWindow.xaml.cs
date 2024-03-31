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
using TaskTreckerUI.Services;
using TaskTreckerUI.Views;
using static System.Net.Mime.MediaTypeNames;

namespace TaskTreckerUI
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
            if(LocalConnectionService.Adress is null)
            {
                Close();
                return;
            }
            if (AuthService.User is null)
            {
                var auth = new AuthWindow();
                auth.ShowDialog();
                if (!auth.IsAuthorized) Close();
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
           
        }
    }
}