using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Services
{
    public class UserManagerService
    {
        private readonly IUserRepository _userRepository;
        public UserManagerService(IUserRepository repository)
        {
            _userRepository = repository;
        }

    }
}
