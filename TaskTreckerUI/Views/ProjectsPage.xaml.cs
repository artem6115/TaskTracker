using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPage.xaml
    /// </summary>
    public partial class ProjectsPage : Page
    {
        Navigator _navigator;
        ProjectVm _context;
        public ProjectsPage(Navigator navigator)
        {
            InitializeComponent();
            _context = DataContext as ProjectVm;
            _navigator = navigator;
        }

        private void Open_MyPartProj_btn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var proj = List_MyPartProj.SelectedItem as ProjectDto;
                if (proj == null) return;
                _navigator.Open(new EpicPage(proj, _navigator,true));

            }
        }
        private void Open_MyPartProj_mouse(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var proj = List_MyPartProj.SelectedItem as ProjectDto;
                if (proj == null) return;
                _navigator.Open(new EpicPage(proj, _navigator,true));

            }
        }
        private async void Open_MyProj_btn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var proj = List_MyProj.SelectedItem as ProjectDto;
                if (proj == null) return;
                _navigator.Open(new EpicPage(proj,_navigator),false);
            }
        }
        private async void Open_MyProj_mouse(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var proj = List_MyProj.SelectedItem as ProjectDto;
                if (proj == null) return;
                _navigator.Open(new EpicPage(proj, _navigator));


            }
        }
        private async void Create_Project(object sender, EventArgs e)
            => await CreateProject();
        private async Task CreateProject(ProjectDto project = null)
        {
            var editWindow = new PageEditWindow(project?.Id);
            editWindow.ShowDialog();
            if (editWindow.Model is not null)
            {
                ProjectDto newProject;

                newProject = await ProjectService.CreateProject(editWindow.Model);
                if (newProject is null)
                {
                    _navigator.AddError("Не удалось создать проект");
                    return;
                }
                _context.MyProjects.Add(newProject);
                _navigator.AddInformation("Проект успешно создан");
                           
                
            }

        }


    }
}
