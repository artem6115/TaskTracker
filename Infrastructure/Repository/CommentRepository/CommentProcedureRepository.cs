using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.CommentRepository
{
    public class CommentProcedureRepository : ICommentRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<CommentProcedureRepository> _logger;
        public CommentProcedureRepository(TaskTrackerDbContext context, ILogger<CommentProcedureRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Comment> GetComment(long Id)
        {
            var entity = await _context.Comments
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == Id);
            if (entity == null)
                throw new FileNotFoundException("Коментарий не найден");
            return entity;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            var Id = await _context.Create_Comment(comment);
            var newComment = await GetComment(Id);
            _logger.LogInformation($"Create Id: {newComment.Id}, description: {newComment.Description}");
            return newComment;

        }

        public async Task DeleteComment(long Id)
        {
            var comment = await GetComment(Id);
            if (comment.User.Id != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете удалить данный коментарий");
            await _context.Delete_Comment(Id);
            _logger.LogInformation($"Deleted comment Id: {comment.Id}, description: {comment.Description}");
        }

        public async Task<List<Comment>> GetCommentsForTask(long TaskId)
            => await _context.Comments
            .Include(x => x.User)
            .AsNoTracking()
            .Where(x => x.WorkTaskId == TaskId)
            .ToListAsync();

        public async Task<Comment> UpdateComment(Comment comment)
        {
            await _context.Update_Comment(comment);
            _logger.LogInformation($"Updated Id: {comment.Id}, description: {comment.Description}");
            return comment;
        }
    }
}
