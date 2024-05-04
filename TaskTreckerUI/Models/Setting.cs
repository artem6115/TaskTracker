using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTrackerUI.Models
{


    public class Setting
    {
        static string _work_Path = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.ApplicationData), "TaskTracker");
        static string _setting_file_Path = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.ApplicationData), "TaskTracker", "config.json");

        bool _updateForOpen = true;
        bool _updateForNavigate;
        bool _updateAuto;
        int _delaySecond = 300;
        string? _adrees;
        string? _token; 
        string? _refreshToken;

        static Setting()
        {
            if (!Directory.Exists(_work_Path))
                Directory.CreateDirectory(_work_Path);
 
        }
        public static Setting LoadSettings()
        {
           if(!File.Exists(_setting_file_Path))return new Setting();
            return JsonSerializer.Deserialize<Setting>
                (File.ReadAllText(_setting_file_Path)) ?? new Setting();
        }
        public Setting(){}

        public bool UpdateForOpen { get=>_updateForOpen; set
            {
                _updateForOpen = value;
                Update();
            } }
        public bool UpdateForNavigate { get=>_updateForNavigate; set {
                _updateForNavigate = value;
                Update();
            }
        }
        public bool UpdateAuto { get=>_updateAuto; set
            {
                _updateAuto = value;
                Update();
            }
        }
        public int DelaySecond { get=>_delaySecond; set {
                _delaySecond = value;
                Update();
            }
        }
        public string? Adrees { get=>_adrees; set {
                _adrees = value;
                Update();
            }
        }
        public string? Token { get => _token; set {
                _token = value;
                Update();
            }
        }
        public string? RefreshToken { get=>_refreshToken; set {
                _refreshToken = value;
                Update();
            }
        }
                
        private void Update()
        {
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(_setting_file_Path,json);
        }


    }

}
