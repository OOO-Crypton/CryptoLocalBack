using CryptoLocalBack.Domain;
using ParserBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace XmrigParser
{
    public class Parser : IParser
    {
        public async Task<List<Videocard>?> GetVideocard(string data)
        {
            List<Videocard> res = new()
            {
                new Videocard()
                {
                    Id = Guid.Parse("e3ea1e56-c592-4955-b014-eb961b912c7b"),
                    FullName = "Видеокарта 1",
                    CardManufacturer = "ASUS",
                    CCDType = ManufacturerType.AMD
                },
                new Videocard()
                {
                    Id = Guid.Parse("11ea1e56-c592-4955-b014-eb961b912c7b"),
                    FullName = "Видеокарта 2",
                    CardManufacturer = "MSI",
                    CCDType = ManufacturerType.NVIDIA
                }
            };
            return res;
        }

        public async Task<List<Monitoring>?> Parse(string data, List<Videocard> videocard)
        {
            JsonFromXmrig? json = JsonSerializer.Deserialize<JsonFromXmrig>(data);
            if (json == null)
            {
                return null;
            }
            List<Monitoring> res = new();
            foreach (var item in videocard)
            {
                res.Add(new Monitoring() { IsActive = true, Date = DateTime.Now, CurrentHashrate = json.hashrate.highest ?? 0, Videocard = item });
            }
            return res;
        }
    }
}
