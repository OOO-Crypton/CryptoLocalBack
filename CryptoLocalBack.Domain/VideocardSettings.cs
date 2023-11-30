using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocalBack.Domain
{
    public class VideocardSettings
    {
        public double FanSpeed { get; set; }
        public double FrequencyCore { get; set; }
        public double FrequencyMem { get; set; }
        public double Power { get; set; }

        public VideocardSettings(string[] lines)
        {
            FanSpeed = double.Parse(lines[0].Split("FanSpeed:")[1]);
            FrequencyCore = double.Parse(lines[0].Split("FrequencyCore:")[1]);
            FrequencyMem = double.Parse(lines[0].Split("FrequencyMem:")[1]);
            Power = double.Parse(lines[0].Split("Power:")[1]);
        }

    }
}
