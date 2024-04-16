using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.ViewModels
{
    public class NoteVm : VMBase
    {
        public string? FindText { get; set; }
        ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes { get => _notes;
            set {
                _notes = value;
                _notes.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => Refresh();
                Refresh();
                } 
            }
        ObservableCollection<Note> _notesView;
        public ObservableCollection<Note> NotesView { get => _notesView; set { _notesView = value; OnPropertyChanged(); } }
        public override async Task<bool> LoadData() {
            var list = await NoteService.GetNotesAsync();
            if(list is null ) return false;        
            Notes = new ObservableCollection<Note>(list);
            return true;  
        }

        public void Refresh()
        {

            if (string.IsNullOrWhiteSpace(FindText))
                NotesView = Notes;
            else
                NotesView = new ObservableCollection<Note>
                    (Notes.Where(x=>x.Description.Contains(FindText.Trim(), StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
