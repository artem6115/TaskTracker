using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(long Id);
        public Task<User> GetUserByLoginAsync(string email);

        public Task<List<User>> GetAllUsersAsync();

        public Task<User> AddUserAcync(User user);
        public Task UpdateUserAcync(User user);
        public Task UpdateUserAccountAcync(User user);
        public Task DeleteUserAcync(User user);
        Task Delete_RefreshToken();
    }
}
