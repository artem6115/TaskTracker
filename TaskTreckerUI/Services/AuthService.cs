using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TaskTrackerUI.Models;
using TaskTrackerUI.Views;

namespace TaskTrackerUI.Services
{
    internal static class AuthService
    {
        public static User? User { get; private set; }
        public static string Work_Path { get; private set; }
        public static string TokenPath { get; private set; }
        public static string RefreshTokenPath { get; private set; }
        private static string? token = null;
        private static string? refreshToken = null;
        static AuthService() {

            Work_Path = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "TaskTracker");
            TokenPath = Path.Combine(Work_Path, "Token.txt");
            RefreshTokenPath = Path.Combine(Work_Path, "RefreshToken.txt");
            if (!Directory.Exists(Work_Path))
                Directory.CreateDirectory(Work_Path);
            if (File.Exists(TokenPath))
                token = File.ReadAllText(TokenPath);
            if (File.Exists(RefreshTokenPath))
                refreshToken = File.ReadAllText(RefreshTokenPath);
        }
        private static async Task<bool> LoginWithToken(HttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Auth/GetUser");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    User = await result.Content.ReadFromJsonAsync<User>();
                    return true;
                }
            }
            catch { }
            return false;
        }
        public async static Task<bool> TryAuthorizeWithJwtTokens()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient httpClient = new HttpClient(clientHandler,true))
            {

                if (await LoginWithToken(httpClient))
                    return true;
                if (await TryRefreshTokens(httpClient))
                {
                    if (await LoginWithToken(httpClient))
                        return true;
                }

                return false;
            }
        }

        private async static Task<bool> TryRefreshTokens(HttpClient httpClient) {

            if(string.IsNullOrWhiteSpace(refreshToken)) return false;

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Auth/RefreshToken");
                request.Content =  JsonContent.Create<string>(refreshToken);
                var result = await httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    var authResult = await result.Content.ReadFromJsonAsync<AuthResult>();
                    if(authResult is null) return false;
                    token = authResult.Token;
                    refreshToken = authResult.RefreshToken;
                    SaveTokens();
                    return true;
                }
            }
            catch { }

            return false;


        }

        private static async void SaveTokens()
        {
            await File.WriteAllTextAsync(TokenPath, token);
            await File.WriteAllTextAsync(RefreshTokenPath, refreshToken);

        }

        public static async Task<AuthResult> CreateUser(UserDto user)
        {
           
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient httpClient = new HttpClient(clientHandler,true);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{LocalConnectionService.Adress}/api/Auth/Regist");
            request.Content = JsonContent.Create<UserDto>(user);
            try
            {
                var result = await httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    var authResult = await result.Content.ReadFromJsonAsync<AuthResult>();
                    token = authResult.Token;
                    refreshToken = authResult.RefreshToken;
                    SaveTokens();
                    if (!await TryAuthorizeWithJwtTokens())
                        authResult.ErrorMessage = "Ошибка входа. Учетная запись создана, войдите в систему";

                    return authResult;
                }

                var errorMassage = await result.Content.ReadAsStringAsync();
                return new AuthResult() { ErrorMessage = errorMassage };

            }
            catch {
                return new AuthResult() {
                    ErrorMessage= "Соеденение с сервером потеряно"
                }; 
            }
            finally { httpClient.Dispose(); }
        }
        public static async Task<AuthResult> Login(UserDto user)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Auth/Login");
            request.Content = JsonContent.Create<UserDto>(user);
            HttpClient httpClient = new HttpClient(clientHandler, true);
            try
            {
                var result = await httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    var authResult = await result.Content.ReadFromJsonAsync<AuthResult>();
                    token = authResult.Token;
                    refreshToken = authResult.RefreshToken;
                    SaveTokens();
                    if (!await TryAuthorizeWithJwtTokens())
                        authResult.ErrorMessage = "Ошибка входа";

                    return authResult;
                }

                var errorMassage = await result.Content.ReadAsStringAsync();
                return new AuthResult() { ErrorMessage = errorMassage };

            }catch {
                return new AuthResult()
                {
                    ErrorMessage = "Соеденение с сервером потеряно"
                };
            }
            finally { httpClient.Dispose(); }
        }

        public static async Task<T> SendAsync<T>(HttpRequestMessage massage) where T : class
        {
            var result = await SendAsync(massage);
            if (result is not null && result.IsSuccessStatusCode)
                return await result.Content.ReadFromJsonAsync<T>();
            return null!;
        }
        public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage massage) 
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            massage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpClient = new HttpClient(clientHandler, true);
            try
            {
                var result = await httpClient.SendAsync(massage);

                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var resultRefresh = await TryRefreshTokens(httpClient);
                    User = null!;
                    if (resultRefresh)
                    {
                        await LoginWithToken(httpClient);
                        massage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        result = await httpClient.SendAsync(massage);

                    }
                    else
                    {
                        var window = new AuthWindow();
                        window.ShowDialog();
                        if (User is not null)
                        {
                            var newMessage = new HttpRequestMessage(massage.Method,massage.RequestUri);
                            newMessage.Content = massage.Content;
                            return await SendAsync(newMessage);
                        }

                    }

                }
                return result;

            }
            catch { }
            finally { clientHandler.Dispose();}
            return null!;
        }
        public static async Task<(bool,string)> RemovePassword(string newPassword,string oldPassword)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Auth/RemovePassword");
            message.Content = JsonContent.Create(new {newPassword,oldPassword});
            var result = await SendAsync(message);
            if (result is null) return (false, null!); ;
            if (result.IsSuccessStatusCode)
                return (true, null!);
            var errorMassage = await result.Content.ReadAsStringAsync();
            return (false, errorMassage);
        }

    }
}
