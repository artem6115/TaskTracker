using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        NoteVm _context;
        public NotesPage()
        {
            InitializeComponent();
            _context = DataContext as NoteVm;
            LoadData();
        }

        private async void LoadData()
        {
            var notes = await NoteService.GetNotesAsync();
            _context.Notes = new ObservableCollection<Note>(notes);
        }

        private async void Send(object sender, RoutedEventArgs e)
        {
            await NoteService.CreateNoteAsync(new Note() { Description = Note_Text.Text}) ;
            LoadData();
        }
    }
}
