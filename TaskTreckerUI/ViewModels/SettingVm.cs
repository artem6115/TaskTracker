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
    public class SettingVm : VMBase
    {
        public User User { get; private set; } = AuthService.User;
        public Setting Setting { get; private set; } = SettingService.Setting;
        public List<UpdatePeriod> Periods { get; set; }
        public SettingVm()
        {
            Periods = new List<UpdatePeriod>() {
                new UpdatePeriod("10 мин",600),
                new UpdatePeriod("5 мин",300),
                new UpdatePeriod("1 мин",60),
                new UpdatePeriod("30 сек",30),
                new UpdatePeriod("20 сек",20),
                new UpdatePeriod("10 сек",10),
                new UpdatePeriod("5 сек",5)

                };
        }

        public override async Task<bool> LoadData() {

            return true;  
        }
    }
    public record UpdatePeriod(string title,int value);
}
