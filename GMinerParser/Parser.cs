using CryptoLocalBack.Domain;
using ParserBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GMinerParser
{
    public partial class Parser : IParser
    {
        public async Task<List<Videocard>?> GetVideocard(string data)
        {
            List<Videocard> res = new();
            
            return res;
        }

        public async Task<List<Monitoring>?> Parse(string data, List<Videocard> videocard)
        {
            List<Monitoring> res = new();
            foreach ()
            {
                Monitoring mon = new()
                {
                    Date = DateTime.Now,
                    IsActive = true,
                    CurrentHashrate = double.Parse(fullData.Count > 5 ? fullData[5].Value : "0"),
                    GPUTemperature = double.Parse(fullData[1].Value ?? "0"),
                    FanRPM = double.Parse(fullData[2].Value ?? "0"),
                    EnergyConsumption = double.Parse(fullData[3].Value ?? "0"),
                    Videocard = videocard[index]
                };
                Console.WriteLine(mon.ToString());
                res.Add(mon);
            } 
            return res;
        }
    }
}
