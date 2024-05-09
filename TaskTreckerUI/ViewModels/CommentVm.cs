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
    public class CommentVm : VMBase
    {
        public long TaskId { get; set; }
        ObservableCollection<Comment> _comments;
        public ObservableCollection<Comment> Comments 
            { get=>_comments; set { _comments = value;OnPropertyChanged(); } }
        public async override Task<bool> LoadData()
        {
            var comments = await CommentService.GetComments(TaskId);
            if(comments == null) return false;
            Comments = new ObservableCollection<Comment>(comments.OrderBy(x=>x.Date));
            return true;
        }
    }
}
