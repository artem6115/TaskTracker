using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using TaskTrackerUI.Models;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Services
{
    public class Navigator
    {
        Frame _frame;
        TextBlock _title;
        Stack<Page> BackPage = new Stack<Page>();
        Stack<Page> NextPage = new Stack<Page>();
        bool _willUpdate = true;
        DispatcherTimer _timer;
        public Page CurrentPage { get; private set; }
        public Navigator(Frame frame, TextBlock title, DispatcherTimer timer)
        {
            _frame = frame;
            _title = title;
            _timer = timer;
            _timer.Tick += (s,e)=>LoadData();
            _timer.Interval = new TimeSpan(0, 0, SettingService.Setting.DelaySecond);
            if(SettingService.Setting.UpdateAuto)
                _timer.Start();
        }
        public async void Open(Page page, bool AutoLoadData = true)
        {
            if(CurrentPage is not null) BackPage.Push(CurrentPage);
            CurrentPage = page;
            Navigate();
            if(SettingService.Setting.UpdateForOpen && AutoLoadData)
                await LoadData();

        }
        public async void Back()
        {
            if (BackPage.Count == 0) return;
            NextPage.Push(CurrentPage);
            CurrentPage = BackPage.Pop();
            if(SettingService.Setting.UpdateForNavigate)
                await LoadData();
            Navigate();
        }
        public async void Next()
        {
            if (NextPage.Count == 0) return;
            BackPage.Push(CurrentPage);
            CurrentPage = NextPage.Pop();
            if (SettingService.Setting.UpdateForNavigate)
                await LoadData();
            Navigate();
        }
        public async Task<bool> LoadData()
        {
            if (CurrentPage is null || CurrentPage.DataContext is null) return true ;

            if (!_willUpdate)
            {
                SetTitle(true);
                AddInformation("Страница не была загружена (отключена загрузка в настройках)");
                return true;
            }
            AddInformation("Загрузка...");
            var loadingPage = CurrentPage;
            VMBase context = CurrentPage.DataContext as VMBase;
            var updateResult = await context.LoadData();
            if (updateResult == false && CurrentPage == loadingPage)
                SetTitle(false);
            else
                SetTitle(true);

            return updateResult;
        }
        private void Navigate()
        { 
            _frame.Navigate(CurrentPage);
            SetTitle(true);
        }
        public void SetTitle(bool valid)
        {
            if (valid)
            {
                _title.Text = $"{CurrentPage.Title}";
                _title.Foreground = Brushes.White;
            }
            else
            {
                _title.Text = $"{CurrentPage.Title} / Неудалось обновить";
                _title.Foreground = Brushes.Red;
            }
        }
        public void AddInformation (string message)
        {
            if (CurrentPage is null) return;
            _title.Text = $"{CurrentPage.Title} ({message})";
            _title.Foreground = Brushes.White;
        }
        public void AddError(string message)
        {
            _title.Text = $"{CurrentPage.Title} ({message})";
            _title.Foreground = Brushes.Red;
        }

        public void UpdateContinue()
        { 
            _willUpdate = true; 
            if(SettingService.Setting.UpdateAuto)_timer.Start();
         }

        public void UpdateStop()
        { _willUpdate = false; _timer.Stop(); }
        public void StartAutoUpdate()
            =>_timer.Start();
        public void StopAutoUpdate()
            => _timer.Stop();
        public void UpdateDelayTimer()
            => _timer.Interval = new TimeSpan(0, 0, SettingService.Setting.DelaySecond);
    }
}
