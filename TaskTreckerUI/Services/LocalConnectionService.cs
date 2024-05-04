using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Services
{
    internal static class LocalConnectionService
    {
        public static string? Adress { get; set; }

        public static async Task<bool> FindServer(string? ip,CancellationToken token)
        {
            
            if (ip is null)
            {
                ip = SettingService.Setting.Adrees;
                ip ??= "127.0.0.1:5050";
            }
            else
            {
                if (ip.Split('.', ':').Length != 5) return false;
            }
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient httpClient = new HttpClient(clientHandler))
            {
                if (token.IsCancellationRequested) return false;
                try
                {
                    string uri = $"https://{ip}";
                    var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri), token);
                    if (result.IsSuccessStatusCode)
                    {
                        Adress = ip;
                        SettingService.Setting.Adrees = Adress; 
                        return true;
                    }
                }
                catch { }

            }
            return false;
        }

    }
}
