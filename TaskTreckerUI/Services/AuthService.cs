using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskTreckerUI.Models;

namespace TaskTreckerUI.Services
{
    internal static class AuthService
    {
        public static User? User { get; private set; }
        public async static Task<bool> TryAuthorizeWithJwtTokens()
        {
            
            var path =  Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "TaskTrecker");
            var tokenPath = Path.Combine(path, "Token.txt");
            var refreshTokenPath = Path.Combine(path, "RefreshToken.txt");
            string? token;
            string? refreshToken;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if(File.Exists(tokenPath))
                token = File.ReadAllText(tokenPath);
            if (File.Exists(refreshTokenPath))
                refreshToken = File.ReadAllText(refreshTokenPath);;
            token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJGdWxsTmFtZSI6ItCc0LjQutC-0LIg0JDRgNGC0ZHQvCDQkNC90LTRgNC10LXQstC40YcgIiwiRW1haWwiOiJtaWtvdkAyMDAzLnJ1IiwiSWQiOiIxMiIsIm5iZiI6MTcxMTkxMzEyMCwiZXhwIjoxNzExOTE2NDIwLCJpc3MiOiJUYXNrVHJhY2tlclNlcnZlciIsImF1ZCI6IlRhc2tUcmFja2VyQ2xpZW50cyJ9.tzSo30Oat8ZqadYFDwzzFwHIyFkzac_c3RD5f-pImFY";
            if (string.IsNullOrWhiteSpace(token)) return false;

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient httpClient = new HttpClient(clientHandler);

            for(int i = 0; i < 5; i++)
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}:5050/api/Auth/GetUser");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var result = await httpClient.SendAsync(request);
                    User = await  result.Content.ReadFromJsonAsync<User>();
                    return true;
                }
                catch { }

            }

            return false;
        }
    }
}
