using CryptoLocalBack.Domain;
using CryptoLocalBack.Helpers;
using CryptoLocalBack.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParserBase
{
    public class HttpBase
    {
        private readonly string baseUrl;
        private readonly IParser parser;
        private readonly HttpClient httpClient;
        public HttpBase(IParser parser, string url)
        {
            httpClient = new HttpClient();
            this.parser = parser;
            this.baseUrl = url; 
        }
        
        public async Task<bool> StartWithDelayAsync(int sleep = 5000)
        {
            using CryptoLocalBackDbContext db = new CryptoLacalBackFactory().CreateDbContext(Array.Empty<string>());
            db.Database.EnsureCreated();
            List<Videocard> videocard = await GetVideocardsAsync(db);
            if (videocard.Count == 0)
            {
                Console.WriteLine("Can\'t find videocard!");
                return false;
            }
            //string hostName = Dns.GetHostName();
            //string? IP = Dns.GetHostByName(hostName)?.AddressList[0].ToString();
            //Console.WriteLine("MY_IP: " + IP);
            TcpListener tcpListener = new(IPAddress.Any, 44444);
            tcpListener.Start();
            while (true)
            {
                List<Monitoring>? mon = await GetMonAsync(videocard);
                if (mon == null || !mon.Any())
                {
                    Console.WriteLine("Monitoring was null"); 
                    tcpListener.Stop();
                    return true;
                }
                _ = Task.Run(async () => await SendTCP(tcpListener));

                await db.AddRangeAsync(mon);
                await db.SaveChangesAsync();
                await Task.Delay(sleep);
            }
        }

        private static async Task SendTCP(TcpListener tcpListener)
        {
            try
            {
                using CryptoLocalBackDbContext db = new CryptoLacalBackFactory().CreateDbContext(Array.Empty<string>());
                var tcp = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine("\n****************************\n\nclient: " + tcp.Client.RemoteEndPoint);
                List<VideocardView> video = db.Videocards
                    .Include(x => x.Monitorings)
                    .Select(x => new VideocardView(x, x.Monitorings.OrderBy(x => x.Date).LastOrDefault() ?? new()))
                    .ToList();
                await SendToFrontAsync(tcp, video);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task<List<Videocard>> GetVideocardsAsync(CryptoLocalBackDbContext db)
        {
            string? data = await GetJsonData();
            if (data == null)
            {
                return new();
            }
            List<Videocard> monVideo = await parser.GetVideocard(data) ?? new();
            List<Videocard> items = await db.Videocards.ToListAsync();
            foreach (Videocard item in items)
            {
                db.Videocards.Remove(item);
            }
            foreach (var item in monVideo)
            {
               await db.Videocards.AddAsync(item);
            }
            await db.SaveChangesAsync();
            return monVideo;
        }
        private async Task<List<Monitoring>?> GetMonAsync(List<Videocard> videocards)
        {
            string? responseBody = await GetJsonData();
            //Console.WriteLine($"{responseBody}");
            if (responseBody == null) 
            { 
                return null;
            }
            List<Monitoring>? monitoring = await parser.Parse(responseBody, videocards);
            return monitoring;
        }

        /// <summary>
        /// Будет пытаться получить данные с api 5 раз с интервалом в секунду
        /// </summary>
        /// <returns></returns>
        private async Task<string?> GetJsonData()
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
            return null;
        }

        private static async Task SendToFrontAsync(TcpClient tcpClient, List<VideocardView> vid)
        {
            string json = JsonConvert.SerializeObject(vid);
            NetworkStream writer = tcpClient.GetStream();
            await writer.WriteAsync(Encoding.UTF8.GetBytes(json));
            Console.WriteLine(json);
            await writer.FlushAsync();
        }
    }
}
