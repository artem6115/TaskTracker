using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface ICommentRepository
    {
        public Task<Comment> GetComment(long Id);
        public Task<List<Comment>> GetCommentsForTask(long TaskId);
        public Task<Comment> CreateComment(Comment comment);
        public Task<Comment> UpdateComment(Comment comment);
        public Task DeleteComment(long Id);


    }
}
