using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TaskTreckerUI.Services
{
    internal static class LocalConnectionService
    {
        public static string[] AllAdress { get; set; }
        public static string? Adress { get; set; }


        static LocalConnectionService()
       {
            string Host = System.Net.Dns.GetHostName();
            AllAdress = System.Net.Dns.GetHostByName(Host).AddressList
                .Where(x => !x.IsIPv6LinkLocal)
                .Select(x=>x.ToString())
                .ToList().Append("127.0.0.1").ToArray();
       }
        public static async Task<bool> FindServer(CancellationToken token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient httpClient = new HttpClient(clientHandler))
            {
                foreach (var ip in AllAdress.Reverse())
                {
                    if (token.IsCancellationRequested) return false;
                    try
                    {
                        string path = $"https://{ip}:5050";
                        var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, path), token);
                        if (result.IsSuccessStatusCode)
                        {
                            Adress = ip;
                            return true;
                        }
                    }
                    catch { }



                }
            }

            //var parallel = Parallel.ForEachAsync(AllAdress, async (ip, state) =>
            //{

            //    HttpClientHandler clientHandler = new HttpClientHandler();
            //    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            //    HttpClient httpClient = new HttpClient(clientHandler);
            //    string path = $"https://{ip}:5050";
            //    try
            //    {
            //        var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, path), token);
            //        if (result.IsSuccessStatusCode)
            //        {
            //            Adress = ip;
            //        }
            //    }
            //    finally { }
            //    {
            //        httpClient.Dispose();
            //    }


            //});
            //parallel.Wait();

            //var tasks = AllAdress.Select(async ip =>
            //{
            //    if (token.IsCancellationRequested) return;
            //    HttpClientHandler clientHandler = new HttpClientHandler();
            //    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            //    HttpClient httpClient = new HttpClient(clientHandler);
            //    try
            //    {

            //        string path = $"https://{ip}:5050";
            //        var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, path), token);
            //        if (result.IsSuccessStatusCode)
            //        {
            //            Adress = ip;

            //        }
            //    }
            //    catch { }
            //    finally { httpClient.Dispose(); }
            //}).ToArray();
            //Task.WaitAll(tasks);


            if (Adress != null)
                return true;

            return false;
        }

    }
}
