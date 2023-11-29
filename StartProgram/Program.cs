using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using CryptoLocalBack.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace StartProgram
{
    internal partial class Program
    {
        //static Regex cpuRegex = CpuRegex();

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork && !ip.ToString().StartsWith("127"))
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        private static async Task Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();

            Console.WriteLine("******************************************");
            Console.WriteLine("*                                        *");
            Console.WriteLine("*   Добро пожаловать в систему CRYPTON   *");
            Console.WriteLine("*                                        *");
            Console.WriteLine("******************************************\n\n");

            if (File.Exists(configuration.GetSection("RegisterPath").Value))
            {
                Console.WriteLine("Вы уже зарегистрированы в системе!");
                return;
            }
            Console.WriteLine("Для продолжения работы, пожалуйста введите RIG ID...");
            bool isNotSended = true;
            while (isNotSended)
            {
                Console.Write("FARM ID: ");
                string key = Console.ReadLine() ?? "";
                string localSystemAddress = GetLocalIPAddress();

                Console.WriteLine("Сбор данных о системе...");

                string mother = File.ReadAllText("/sys/devices/virtual/dmi/id/board_vendor") 
                    + File.ReadAllText("/sys/devices/virtual/dmi/id/board_name")
                    + File.ReadAllText("/sys/devices/virtual/dmi/id/board_version");

                string cpu = (await Helpers.StartCommand("inxi", "-C")).Stdout ?? "cant get cpu";
                SystemData sys = new()
                {
                    Motherboard = mother,
                    CPU = cpu,
                    OSVersion = configuration.GetSection("OSVersion").Value ?? "err"
                };
                Console.WriteLine($"Проверка ключа активации...");

                HttpClient client = new();
                var formContent = new
                { 
                    LocalSystemID = key,
                    LocalSystemAddress = localSystemAddress, 
                    SystemInfo = sys
                };
                try
                {
                    HttpResponseMessage res = await client.PostAsync(configuration.GetSection("ServerAddress").Value ?? "err", JsonContent.Create(formContent));
                    if (!res.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Ошибка активации! Попробуйте еще");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Сервер недоступен!");
                    return;
                }
                isNotSended = false;
            }
            try
            {
                if (!File.Exists(configuration.GetSection("DriversPath").Value))
                {
                    Console.WriteLine("Скачивание драйверов...");
                    if (string.IsNullOrEmpty((await Helpers.StartCommand("sudo", $"sed -i -e 's/\r$//' {configuration.GetSection("GetDriversPath").Value}")).Stderr)
                        || string.IsNullOrEmpty((await Helpers.StartCommand("sudo", $"{configuration.GetSection("GetDriversPath").Value}")).Stderr))
                    {
                        Console.WriteLine("Ошибка при скачивании драйверов");
                        throw new Exception();
                    }
                    await Helpers.StartCommand("touch", $"{configuration.GetSection("DriversPath").Value}");
                }

                Console.WriteLine("Запуск системы...");
                if ((await Helpers.StartCommand("sudo", $"systemctl enable backend.service")).ExitCode != 0
                    || (await Helpers.StartCommand("sudo", $"systemctl restart backend.service")).ExitCode != 0)
                {
                    Console.WriteLine("Ошибка при запуске системы");
                    throw new Exception();
                }

                await Helpers.StartCommand("touch", $"{configuration.GetSection("RegisterPath").Value}");
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так!");
                throw;
            }
            Console.WriteLine("Все системы успешно запущены!");
        }

        //[GeneratedRegex(@"Manufacturer: (?<man>\w+)\s*Product Name: (?<prod>\w+)")]
        //private static partial Regex CpuRegex();
    }
}