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
    public class NotifyVm : VMBase
    {
        ObservableCollection<Notify> _notifies;
        public ObservableCollection<Notify> Notifies { get=>_notifies; set { _notifies = value; OnPropertyChanged(); } }
        public async override Task<bool> LoadData() {
            var notifies = await NotifyService.GetNotifies();
            if (notifies is null) return false;
            Notifies = new ObservableCollection<Notify>(notifies.OrderByDescending(x=>x.Date));
            return true;
        }
    }
}
