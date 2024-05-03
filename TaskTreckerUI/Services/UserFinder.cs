using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public class UserFinder
    {
        List<User> UsersCash = new List<User>();
        public List<User> GetUsers()
            => UsersCash.TakeLast(5).ToList();
        public async Task<List<User>> GetUsers(string EmailPattern)
        { 
            if(string.IsNullOrWhiteSpace(EmailPattern)) return GetUsers();
            var result = UsersCash.Where(x=>x.Email.Contains(EmailPattern)).Take(15).ToList();
            if (result.Count >= 5) return result;
            result = await FindUsers(EmailPattern);
            return result?.Take(10).ToList();
        }
        public async Task<List<User>> FindUsers(string EmailPattern)
        {
            var result = await UserService.FindUsers(EmailPattern);
            if (result is null || result.Count == 0) return null!;
            foreach(var user in result)
                if(UsersCash.Find(x=>x.Email==user.Email) == null)UsersCash.Add(user);
            return result;
        }



    }
}
