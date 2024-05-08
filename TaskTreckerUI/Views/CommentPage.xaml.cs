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
using System.Xml.Linq;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для CommentPage.xaml
    /// </summary>
    public partial class CommentPage : Page
    {
        Comment? CurrentComment;
        CommentVm _context;
        Navigator Navigator;
        public CommentPage(Navigator navigator,TaskInfo Task)
        {
            InitializeComponent();
            this.Navigator = navigator;
            _context = DataContext as CommentVm;
            _context.TaskId = Task.Id;
            if(Task.Epic is null )
                Title = $"Мои задачи / {Task.Title} / Коментарии";
            else
                Title = $"{Task.Epic.Project.Name} / {Task.Epic.Title} / {Task.Title} / Коментарии";
        }

        private async void Send(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Comment_Text.Text)) return;
            if (CurrentComment != null)
            {
                CurrentComment.Description = Comment_Text.Text;
                var comment = await CommentService.UpdateComment(CurrentComment);
                if (comment is null)
                {
                    Navigator.AddError("Редактировать коментарий не удалось");
                    return;
                }
                Navigator.AddInformation("Коментарий отредактирован");
                var currentUpdateEntity = _context.Comments.Single(x => x.Id == comment.Id);
                _context.Comments.Insert(_context.Comments.IndexOf(currentUpdateEntity), comment);
                _context.Comments.Remove(currentUpdateEntity);
                CurrentComment = null;

            }
            else
            {
                var createComment = new Comment() {
                    WorkTaskId = _context.TaskId,
                    Description = Comment_Text.Text
                    };
                var comment = await CommentService.CreateComment(createComment);
                if (comment is null)
                {
                    Navigator.AddError("Добавить коментарий не удалось");
                    return;
                }
                Navigator.AddInformation("Коментарий добавлен");
                _context.Comments.Add(comment);
                CommentList.SelectedIndex = _context.Comments.Count-1;
            }
            Comment_Text.Text = "";

        }

        private void Add_Comment(object sender, RoutedEventArgs e)
        {
            Comment_Text.Text = "";
            CurrentComment = null;
        }
        private bool AcceseCheck(Comment comment)
        {
            if (!comment.IsMyComment)
            {
                MessageBox.Show("Вы не можете управлять чужим коментарием","Access debind",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private async void Delete_Comment(object sender, RoutedEventArgs e)
        {
            if (CommentList.SelectedItem is null) return;
            var comment = CommentList.SelectedItem as Comment;
            if (!AcceseCheck(comment)) return;
            if (MessageBox.Show(
                "Удалить коментарий?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question
                ) != MessageBoxResult.Yes) return;

            bool result = await CommentService.DeleteComment(comment.Id);
            if (result)
            {
                _context.Comments.Remove(_context.Comments.Single(x => x.Id == comment.Id));
                Navigator.AddInformation("Коментарий удален");
            }
            else Navigator.AddError("Удаление не удалось");

        }

        private void Edit_Comment(object sender, RoutedEventArgs e)
        {
            if (CommentList.SelectedItem is null) return;
            var comment = CommentList.SelectedItem as Comment;
            if (!AcceseCheck(comment)) return;
            CurrentComment = comment;
            Comment_Text.Text = CurrentComment.Description;
        }

        private void Select_Comment(object sender, SelectionChangedEventArgs e)
            => CurrentComment = null;
    }
}
