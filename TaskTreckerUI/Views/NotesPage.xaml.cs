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
        Navigator Navigator;
        public NotesPage(Navigator navigator)
        {
            Navigator = navigator;
            InitializeComponent();
            _context = DataContext as NoteVm;
        }


        private async void Send(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Note_Text.Text)) return;
            if (CurrentNote != null)
            {
                CurrentNote.Description = Note_Text.Text;
                var note = await NoteService.UpdateNoteAsync(CurrentNote);
                if(note is null)
                {
                    Navigator.AddError("Редактирование заметки не удалось");
                    return;
                }
                Navigator.AddInformation("Заметка отредактирована");
                var currentUpdateEntity = _context.Notes.Single(x=>x.Id == note.Id);
                _context.Notes.Insert(_context.Notes.IndexOf(currentUpdateEntity),note);
                _context.Notes.Remove(currentUpdateEntity);
                CurrentNote = null;

            }
            else
            {
                var createNote = new Note() { Description = Note_Text.Text };
                createNote.WrappDescription();
                var note = await NoteService.CreateNoteAsync(createNote);
                if(note is null)
                {
                    Navigator.AddError("Добавление заметки не удалось");
                    return;
                }
                Navigator.AddInformation("Заметка добавлена");
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

            bool result = await NoteService.DeleteNoteAsync(note);
            if(result) {
                _context.Notes.Remove(_context.Notes.Single(x=>x.Id == note.Id));
                Navigator.AddInformation("Запись удалена");
            }
            else Navigator.AddError("Удаление не удалось");

        }

        private void Edit_Note(object sender, RoutedEventArgs e)
        {
            if (NotesList.SelectedItem is null) return;
            CurrentNote = NotesList.SelectedItem as Note;
            Note_Text.Text = CurrentNote.Description;
        }

        private void Select_Note(object sender, SelectionChangedEventArgs e)
            => CurrentNote = null;

        private void Filter_Note(object sender, RoutedEventArgs e)
        {
            _context.FindText = Find_box.Text;
            _context.Refresh();
        }

        private void DropFilter_Note(object sender, RoutedEventArgs e)
        {
            Find_box.Text = "";
            _context.FindText = null;
            _context.Refresh();
        }

        private void New_Line(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Note_Text.Text += Environment.NewLine;
            }
        }
    }
}
