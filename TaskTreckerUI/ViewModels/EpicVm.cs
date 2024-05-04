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
    public class EpicVm : VMBase
    {
         ObservableCollection<Epic>? _epics;

        public ObservableCollection<Epic>? Epics
            { get=>_epics; set { _epics = value;OnPropertyChanged(); } }
        public ProjectDto? Project { get; set; }
        public async override Task<bool> LoadData()
        {
            if (Project is null) return false;
            Epics = new ObservableCollection<Epic>(
                await EpicService.GetProjectEpics(Project.Id)
                );
            return Epics is not null;
        }
    }
}
