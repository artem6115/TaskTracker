using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public static class NotifyService
    {
        public static async Task<List<Notify>> GetNotifies()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Notify");
            List<Notify> notify = await AuthService.SendAsync<List<Notify>>(request);
            return notify;
        }
        public static async Task ReadAllNotifies()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Notify/ReadAll");
            await AuthService.SendAsync(request);
        }
        public static async Task<bool> DeleteAllNotifies()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Notify/DeleteAll");
            var result = await AuthService.SendAsync(request);
            return result is not null && result.IsSuccessStatusCode;
        }
        public static async Task<bool> DeleteNotify(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"https://{LocalConnectionService.Adress}/api/Notify/{Id}");
            var result = await AuthService.SendAsync(request);
            return result is not null && result.IsSuccessStatusCode;

        }
    }
}
