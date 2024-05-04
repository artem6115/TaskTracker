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
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для EpicEditWindow.xaml
    /// </summary>
    public partial class EpicEditWindow : Window
    {
        Epic _epic;

        public Epic Model { get; private set; }
        public EpicEditWindow(Epic epic)
        {
            _epic = epic;
            InitializeComponent();
            (DataContext as EpicEditVm).Epic = _epic;
        }
        private void Save_Data(object sender, EventArgs e) {
            if (!ValidateModel()) return;
            Model = _epic;
            Close();
        }
        private void Close(object sender, EventArgs e) => Close();
        private bool ValidateModel()
        {
            bool valid = true;
            var error = "";
            if (string.IsNullOrWhiteSpace(_epic.Title))
            {
                valid = false;
                error += "Название эпика не должно быть пустым\n";
            }
            if (_epic.Title?.Length > 20)
            {
                valid = false;
                error += "Название эпика не должно быть больше 20 символов\n";
            }
            if (string.IsNullOrWhiteSpace(_epic.Description))
            {
                valid = false;
                error += "Описание эпика не должно быть пустым\n";
            }
            if (!valid)
                MessageBox.Show(error, "Error list", MessageBoxButton.OK, MessageBoxImage.Warning);
            return valid;
        }

    }
}
