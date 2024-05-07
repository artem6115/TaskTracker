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
        ObservableCollection<User> _users;
        public ObservableCollection<User> Users { get=>_users; set { _users = value;OnPropertyChanged(); } }
        public async override Task<bool> LoadData()
        {
            if (Project is null) return false;
            var result = await EpicService.GetProjectEpics(Project.Id);
            if (result == null) return false;
            Epics = new ObservableCollection<Epic>(result);
            var users = await ProjectService.GetUsers(Project.Id);
            if(users is null )return false;
            Users = new ObservableCollection<User>(users);
            return Epics is not null;
        }
    }
}
