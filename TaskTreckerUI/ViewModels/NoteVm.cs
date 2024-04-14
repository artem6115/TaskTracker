using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.ViewModels
{
    public class NoteVm : VMBase
    {
        ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes { get => _notes; set { _notes = value;OnPropertyChanged(); } }
        public override async Task<bool> LoadData() {
            var list = await NoteService.GetNotesAsync();
            if(list is null ) return false;
            Notes = new ObservableCollection<Note>(list);
            return true;  
        }
    }
}
