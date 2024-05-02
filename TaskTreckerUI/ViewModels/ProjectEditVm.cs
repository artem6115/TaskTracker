using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.ViewModels
{
    public class ProjectEditVm : VMBase
    {
        Project _project;
        public long? ProjectId { get; set; }
        public Project Project { get=>_project; set { _project = value; OnPropertyChanged(); } }

        public async override Task<bool> LoadData()
        {
            if (ProjectId is null)return false;
            Project = await ProjectService.GetProject((long)ProjectId);
            return Project != null;
        }
    }
}
