using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public static class UserService
    {
        public static async Task<List<User>> FindUsers (string EmailPatern)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/User");
            request.Content = JsonContent.Create(EmailPatern);
            List<User> users = await AuthService.SendAsync<List<User>>(request);
            return users;
        }
    }
}
