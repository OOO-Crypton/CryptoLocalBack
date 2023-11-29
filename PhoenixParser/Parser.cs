using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CryptoLocalBack.Domain;
using ParserBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoenixParser
{
    public partial class Parser : IParser
    {
        private Regex getHash = GetAverageSpeed();
        public async Task<List<Videocard>?> GetVideocard(string data)
        {
            var parser = new HtmlParser(); 
            var document = parser.ParseDocument(data);
            List<Videocard> res = new();
            foreach (IElement element in document.QuerySelectorAll("font").Reverse())
            {
                if (element.GetAttribute("color") == "#FFFFFF") break;
                if (element.GetAttribute("color") != "#55FF55") continue;
                string fullName = element.TextContent;
                ManufacturerType type = ManufacturerType.UNKNOWN;
                if (fullName.Contains("NVIDIA"))
                {
                    type = ManufacturerType.NVIDIA;
                }
                if (fullName.Contains("AMD"))
                {
                    type = ManufacturerType.AMD;
                }
                Videocard video = new()
                {
                    FullName = fullName,
                    CCDType = type,
                };
                res.Add(video);
            }
            return res;
        }

        public async Task<List<Monitoring>?> Parse(string data, List<Videocard> videocard)
        {
            List<Monitoring> res = new();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(data);
            int index = 0;
            double hash = 0;
            if (getHash.IsMatch(data))
            {
                try
                {
                    string str = getHash.Match(data).Groups["data"].Value.Replace(".", ",");
                    hash = double.Parse(str);
                }
                catch { }
            }
            //if (lines.Length != videocard.Count) return null;
            foreach (IElement element in document.QuerySelectorAll("font").Reverse())
            {
                if (element.GetAttribute("color") == "#55FF55") break;
                if (element.GetAttribute("color") != "#FF55FF") continue;
                MatchCollection fullData = GetMonData().Matches(element.TextContent.Replace("\n", " "));
                if(fullData.Count == 0) continue;
                Monitoring mon = new()
                {
                    Date = DateTime.Now,
                    IsActive = true,
                    CurrentHashrate = hash,
                    GPUTemperature = double.Parse(fullData[1].Value ?? "0"),
                    FanRPM = double.Parse(fullData[2].Value ?? "0"),
                    EnergyConsumption = double.Parse(fullData[3].Value ?? "0"),
                    Videocard = videocard[index]
                };
                index++;
                Console.WriteLine(mon.ToString());
                res.Add(mon);
            } 
            return res;
        }

        [GeneratedRegex(@"\d+(\.\d+)?")]
        private static partial Regex GetMonData();

        [GeneratedRegex(@"Average speed \(5 min\): (?<data>\d+(\.\d+)?) MH/s")]
        private static partial Regex GetAverageSpeed();
    }
}
