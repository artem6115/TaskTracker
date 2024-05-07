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
    public class ProjectVm : VMBase
    {
        ObservableCollection<ProjectDto> _myProjects;
        ObservableCollection<ProjectDto> _myParticipateProjects;
        public ObservableCollection<ProjectDto> MyProjects { get=>_myProjects; set {
                _myProjects = value;
                OnPropertyChanged();
        } }
        public ObservableCollection<ProjectDto> MyParticipateProjects
        {
            get => _myParticipateProjects; set
            {
                _myParticipateProjects = value;
                OnPropertyChanged();
            }
        }


        public override async Task<bool> LoadData() {
            var resultMy = await ProjectService.GetMyProjects();
            var resultPart = await ProjectService.GetMyParticipateProjects();
            if(resultMy != null) 
                MyProjects = new ObservableCollection<ProjectDto>
                    (resultMy);
            if(resultPart != null)
                MyParticipateProjects = new ObservableCollection<ProjectDto>
                (resultPart);
            return MyProjects != null && MyParticipateProjects != null;
        }
    }
}
