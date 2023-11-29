using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace CryptoLocalBack.Helpers
{
    public static class Helpers
    {
        public static string GetLocalIPAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList
                    .FirstOrDefault(x => x.ToString().StartsWith("10."))?
                    .ToString() ?? "err";
        }

        public static async Task<DockerAnswerView> StartCommand(string command, string param)
        {
            //Console.WriteLine(command + " " + param);
            ProcessStartInfo processInfo = new(command, param)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            using Process process = new();
            process.StartInfo = processInfo;

            process.Start();
            //process.BeginOutputReadLine();
            //process.BeginErrorReadLine();
            string? stdoutx = process.StandardOutput?.ReadToEnd();
            string? stderrx = process.StandardError?.ReadToEnd();

            await process.WaitForExitAsync();
            if (!process.HasExited)
            {
                process.Kill();
            }

            //Console.WriteLine($"Exit code : {process.ExitCode}");
            //Console.WriteLine($"Stdout : {stdoutx}");
            //Console.WriteLine($"Stderr : {stderrx}");
            int exitCode = process.ExitCode;
            process.Close();

            return new DockerAnswerView() { ExitCode = exitCode, Stderr = stderrx, Stdout = stdoutx };
        }
    }
}