using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<CommentRepository> _logger;
        public CommentRepository(TaskTrackerDbContext context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Comment> GetComment(long Id)
        {
            var entity = await _context.Comments
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == Id);
            if(entity == null)
                throw new FileNotFoundException("Коментарий не найден");
            return entity;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {

            var entity = await _context.Comments.AddAsync(comment);
            var task = await _context.Tasks
                .AsNoTracking()
                .SingleAsync(x=>x.Id == comment.WorkTaskId);
            if (task is null)
                throw new FileNotFoundException("Задача не найдена");
            if (task.UserId is not null) {
                var notify = new Notify()
                {
                    Message = $"Пользователь {UserClaims.User.Email}, написал коментарий для задаи : {task.Title}",
                    UserId = (long)task.UserId
                };
                await _context.Notifies.AddAsync(notify);
            }
            await _context.SaveChangesAsync();
            var newComment = await _context.Comments
                .AsNoTracking()
                .Include(x => x.User)
                .SingleAsync(x => x.Id == entity.Entity.Id);
            _logger.LogInformation($"Create Id: {newComment.Id}, description: {newComment.Description}");
            return newComment;

        }

        public async Task DeleteComment(long Id)
        {
            var comment  = await GetComment(Id);
            if (comment.User.Id != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете удалить данный коментарий");
            _context.Remove(comment);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted comment Id: {comment.Id}, description: {comment.Description}");
        }

        public async Task<List<Comment>> GetCommentsForTask(long TaskId)
        {
            var result=await _context.Comments
            .Include(x => x.User)
            .AsNoTracking()
            .Where(x => x.WorkTaskId == TaskId)
            .ToListAsync();
            return result;
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated Id: {comment.Id}, description: {comment.Description}");
            return comment;
        }
    }
}
