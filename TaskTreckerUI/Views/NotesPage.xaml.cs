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
        Note? CurrentNote;
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
            if (string.IsNullOrWhiteSpace(Note_Text.Text)) return;
            if (CurrentNote != null)
            {
                CurrentNote.Description = Note_Text.Text;
                var note = await NoteService.UpdateNoteAsync(CurrentNote);
                _context.Notes.Insert(_context.Notes.IndexOf(CurrentNote),note);
                _context.Notes.Remove(CurrentNote);
                CurrentNote = null;

            }
            else
            {
                var note = await NoteService.CreateNoteAsync(new Note() { Description = Note_Text.Text });
                _context.Notes.Insert(0, note);
            }
            Note_Text.Text = "";

        }

        private void Add_Note(object sender, RoutedEventArgs e)
        {
            Note_Text.Text = "";
            CurrentNote = null;
        }

        private async void Delete_Note(object sender, RoutedEventArgs e)
        {
            if (NotesList.SelectedItem is null) return;
            var note = NotesList.SelectedItem as Note;

            if (MessageBox.Show(
                "Удалить заметку?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question
                ) != MessageBoxResult.Yes) return;

            await NoteService.DeleteNoteAsync(note);
            _context.Notes.Remove(note);
            
        }

        private void Edit_Note(object sender, RoutedEventArgs e)
        {
            if (NotesList.SelectedItem is null) return;
            CurrentNote = NotesList.SelectedItem as Note;
            Note_Text.Text = CurrentNote.Description;
        }

        private void Select_Note(object sender, SelectionChangedEventArgs e)
            => CurrentNote = null;

    }
}
