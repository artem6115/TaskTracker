using Infrastructure.Auth;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(TaskTrackerDbContext context, ILogger<UserRepository> logger)
        { 
            _context = context;
            _logger = logger;
        }

        public async Task AddUserAcync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogTrace($"User added, full name {user.FullName}");
        }

        public async Task DeleteUserAcync(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogTrace($"User deleted, full name {user.FullName}");
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(long Id)
        {
            return await _context.Users.AsNoTracking().SingleAsync(x => x.Id == Id);
        }

        public async Task UpdateUserAccountAcync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogTrace($"User Updated, full name {user.FullName}");
        }

        public async Task UpdateUserAcync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogTrace($"User Updated, full name {user.FullName}");
        }
    }
}
