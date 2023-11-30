using CryptoLocalBack.Domain;
using CryptoLocalBack.Helpers;
using CryptoLocalBack.Model.Models;
using MSI.Afterburner;
using MSI.Afterburner.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CryptoLocalBack.Extensions
{
    public class VideocardExtension
    {
        readonly IConfiguration _configuration;
        public VideocardExtension(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<VideocardSettings> GetCurrentSettings()
        {
            string? path = _configuration.GetSection("PathToGetSettings").Value;
            if (!File.Exists(path))
            {
                throw new Exception();
            }
            string[]? lines = (await Helpers.Helpers.StartCommand("sudo", path)).Stdout?.Split("\n");
            if(lines == null || lines.Length == 0) 
                throw new Exception();
            return new VideocardSettings(lines);
        }

        public async Task<DockerAnswerView> ChangeSettings(OverclockingModel model)
        {
            string? path = _configuration.GetSection("PathToApplySettings").Value;
            if (!File.Exists(path))
            {
                throw new Exception();
            }
            string newPath = path.Replace("applySettings", "genApplySettings");
            string lines = string.Join("\n", File.ReadAllLines(path));
            lines = lines.Replace("{gpuPowerMizerZone}", model.Consumption.ToString());
            lines = lines.Replace("{fanControll}", "1");
            lines = lines.Replace("{fanSpeed}", model.CoolerSpeed.ToString());
            lines = lines.Replace("{memClock3}", model.MemoryFrequency.ToString());
            lines = lines.Replace("{memClock2}", model.MemoryFrequency.ToString());
            File.WriteAllText(newPath, lines);
            return await Helpers.Helpers.StartCommand("sudo", newPath);
        }
    }
}
