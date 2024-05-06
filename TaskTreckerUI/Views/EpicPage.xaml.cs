using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для EpicPage.xaml
    /// </summary>
    public partial class EpicPage : Page
    {
        Navigator _navigator;
        EpicVm _context;
        bool accessDeinedtoChange;
        public EpicPage(ProjectDto Project, Navigator navigator, bool AccessDeined = false)
        {
            InitializeComponent();
            accessDeinedtoChange = AccessDeined;
            Title = $"Проекты / {Project.Name}";
            if(AccessDeined)
                menu_panel.Visibility = Visibility.Hidden;
            _navigator = navigator;
            _context = DataContext as EpicVm;
            _context.Project = Project;
            Description_text.Text = Project.Description;
            LoadData();
        }
        private async void LoadData()
        {
            await _context.LoadData();
            Users_List_To_Change.ItemsSource = _context.Users;
        }
        private void Open_Epic_btn(object sender, KeyEventArgs e)
        {
            if (List_Epics.SelectedItem is null) return;
            var epic = List_Epics.SelectedItem as Epic;
            epic.Project = _context.Project;
            _navigator.Open(new EpicTasksPage(epic,_navigator, accessDeinedtoChange), false);
        }
        private void Open_Epic_mouse(object sender, MouseButtonEventArgs e)
        {
            if (List_Epics.SelectedItem is null) return;
            var epic = List_Epics.SelectedItem as Epic;
            epic.Project = _context.Project;
            _navigator.Open(new EpicTasksPage(epic,_navigator, accessDeinedtoChange), false);
        }
        private async void Create_Epic(object sender, EventArgs e) {
            
            var epicWindow = new EpicEditWindow(new Epic() { Project=_context.Project});
            epicWindow.ShowDialog();
            if (epicWindow.Model is null) return;
            var newEpic = await EpicService.CreateEpic(epicWindow.Model);
            if (newEpic != null)
            {

                _context.Epics.Add(newEpic);
                _navigator.AddInformation("Эпик успешно создан");
            }
            else
                _navigator.AddError("Эпик не был создан");
            
        }
        private async void Edit_Epic(object sender, EventArgs e)
        {
            if(List_Epics.SelectedItem is null)return;
            var selectedEpic = List_Epics.SelectedItem as Epic;
            var epicWindow = new EpicEditWindow(new Epic()
            {
                Description=selectedEpic.Description,
                Project=selectedEpic.Project,
                Id=selectedEpic.Id,
                Title=selectedEpic.Title
            });
            epicWindow.ShowDialog();
            var model = epicWindow.Model;
            if (model is null) return;
            model.Project = _context.Project;
            var newEpic = await EpicService.UpdateEpic(model);
            if (newEpic != null)
            {
                var currentEpic = _context.Epics.Single(x=>x.Id==newEpic.Id);

                _context.Epics.Insert(_context.Epics.IndexOf(currentEpic),newEpic);
                _context.Epics.Remove(currentEpic);
                _navigator.AddInformation("Эпик успешно обновлен");
            }
            else
                _navigator.AddError("Эпик не был обновлен");
        }
        private async void Delete_Epic(object sender, EventArgs e)
        {
            if (List_Epics.SelectedItem is null) return;
            var selectedEpic = List_Epics.SelectedItem as Epic;
            if(MessageBox.Show("Удалить эпик?","Delete epic",MessageBoxButton.YesNo,MessageBoxImage.Question) 
                != MessageBoxResult.Yes)return;
            var result = await EpicService.DeleteEpic(selectedEpic.Id);
            if (result)
            {
                _context.Epics.Remove(_context.Epics.Single(x=>x.Id==selectedEpic.Id));
                _navigator.AddInformation("Эпик успешно удален");
            }
            else
                _navigator.AddError("Эпик не был удален");

        }
        private async void UpdateProj_btn(object sender, EventArgs e)
            => UpdateOrDeleteProject(_context.Project);
        private async void UpdateOrDeleteProject(ProjectDto project) {
            var editWindow = new PageEditWindow(project.Id);
            editWindow.ShowDialog();
            if (editWindow.DoDelete && project != null)
            {
                var result = await ProjectService.DeleteProject(project.Id);
                if (result)
                {
                    _navigator.AddInformation("Проект удален");
                    _context.Epics = null!;
                    _context.Project = null!;
                    _navigator.Back();
                }
                else
                    _navigator.AddError("Не удалось удалить проект");
                return;
            }
            if (editWindow.Model is null) return;
            ProjectDto newProject;
            newProject = await ProjectService.UpdateProject(editWindow.Model);
            if (newProject is null)
            {
                _navigator.AddError("Не удалось обновить проект");
                return;
            }
            _context.Project = newProject;
            Title = $"Проекты / {newProject.Name}";
            _navigator.SetTitle(true);
            _navigator.AddInformation("Проект успешно обновлен");
        }
    }
}
