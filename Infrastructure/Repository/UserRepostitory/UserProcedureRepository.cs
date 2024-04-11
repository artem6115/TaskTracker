using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserProcedureRepository : IUserRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserProcedureRepository(TaskTrackerDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<User> AddUserAcync(User user)
        {
            await _context.Create_User(user);
            _logger.LogTrace($"User added, full name {user.FullName}");
            return await _context.Users.AsNoTracking().SingleAsync(x => x.Email == user.Email);

        }

        public async Task DeleteUserAcync(User user)
        {
            await _context.Delete_User(user.Id);
            _logger.LogTrace($"User deleted, full name {user.FullName}");
        }

        public async Task Delete_RefreshToken()
        {
            var user = await _context.Users.SingleAsync(x => x.Id == UserClaims.User.Id);
            user.RefreshToken = null;
            await _context.Update_User(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(long Id)
        {
            return await _context.Users.AsNoTracking().SingleAsync(x => x.Id == Id);
        }
        public async Task<User> GetUserByLoginAsync(string email)
        {
            return await _context.Users.AsNoTracking().SingleAsync(x => x.Email == email);

        }
        public async Task UpdateUserAccountAcync(User user)
        {
            await _context.Update_Account_User(user);
            _logger.LogTrace($"User account updated, full name {user.FullName}");
        }

        public async Task UpdateUserAcync(User user)
        {
            await _context.Update_User(user);
            _logger.LogTrace($"User updated, full name {user.FullName}");
        }
    }
}
