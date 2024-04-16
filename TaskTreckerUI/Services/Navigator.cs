using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
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
        public Page CurrentPage { get; private set; }
        public Navigator(Frame frame, TextBlock title)
        {
            _frame = frame;
            _title = title;
        }
        public async void Open(Page page)
        {
            if(CurrentPage is not null) BackPage.Push(CurrentPage);
            CurrentPage = page;
            Navigate();
            await LoadData();

        }
        public void Back()
        {
            if (BackPage.Count == 0) return;
            NextPage.Push(CurrentPage);
            CurrentPage = BackPage.Pop();
            Navigate();
        }
        public void Next()
        {
            if (NextPage.Count == 0) return;
            BackPage.Push(CurrentPage);
            CurrentPage = NextPage.Pop();
            Navigate();
        }
        public async Task<bool> LoadData()
        {
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
        private void SetTitle(bool valid)
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
            _title.Text = $"{CurrentPage.Title} ({message})";
            _title.Foreground = Brushes.White;
        }
        public void AddError(string message)
        {
            _title.Text = $"{CurrentPage.Title} ({message})";
            _title.Foreground = Brushes.Red;
        }

    }
}
