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
                MessageBox.Show($"{proj.Name}");
                //_navigator.Open();
            }
        }
        private void Open_MyPartProj_mouse(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var proj = List_MyPartProj.SelectedItem as ProjectDto;
                if (proj == null) return;
                MessageBox.Show($"{proj.Name}");
            }
        }
        private async void Open_MyProj_btn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var proj = List_MyProj.SelectedItem as ProjectDto;
                if (proj == null) return;
                await CreateUpdateProject(proj);

            }
        }
        private async void Open_MyProj_mouse(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var proj = List_MyProj.SelectedItem as ProjectDto;
                if (proj == null) return;
                await CreateUpdateProject(proj);

            }
        }
        private async void Create_Project(object sender, EventArgs e)
            => await CreateUpdateProject();
        private async Task CreateUpdateProject(ProjectDto project = null)
        {
            var editWindow = new PageEditWindow(project?.Id);
            editWindow.ShowDialog();
            if (editWindow.DoDelete && project != null)
            {
                var result = await ProjectService.DeleteProject(project.Id);
                if (result)
                {
                    _navigator.AddInformation("Проект удален");
                    _context.MyProjects.Remove(project);
                }
                else
                    _navigator.AddError("Не удалось удалить проект");
                return;
            }
            if (editWindow.Model is not null)
            {
                ProjectDto newProject;

                if (project is null) {
                    newProject = await ProjectService.CreateProject(editWindow.Model);
                    if (newProject is null)
                    {
                        _navigator.AddError("Не удалось создать проект");
                        return;
                    }
                    _context.MyProjects.Add(newProject);
                    _navigator.AddInformation("Проект успешно создан");

                }
                else {
                    newProject = await ProjectService.UpdateProject(editWindow.Model);
                    if (project is null)
                    {
                        _navigator.AddError("Не удалось обновить проект");
                        return;
                    }
                    var indexProj = _context.MyProjects.IndexOf(project);
                    _context.MyProjects.Insert(indexProj, newProject);
                    _context.MyProjects.Remove(project);
                    _navigator.AddInformation("Проект успешно обновлен");
                }

                
                
            }

        }


    }
}
