using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.ViewModels
{
    public class EpicEditVm : VMBase
    {
        Epic _epic;
        public Epic Epic { get=>_epic; set { _epic = value;OnPropertyChanged(); } }
        public async override Task<bool> LoadData()
            => true;
    }
}
