using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmrigParser
{
    internal class JsonFromXmrig
    {
        public string id { get; set; }
        public string worker_id { get; set; }
        public int uptime { get; set; }
        public bool restricted { get; set; }
        public object resources { get; set; }
        public string[] features { get; set; }
        public object results { get; set; }
        public string algo { get; set; }
        public object connection { get; set; }
        public string version { get; set; }
        public string kind { get; set; }
        public string ua { get; set; }
        public object cpu { get; set; } //["brand"]
        public int donate_level { get; set; }
        public bool paused { get; set; }
        public string[] algorithms { get; set; }
        public hashrate hashrate { get; set; } //"highest"
        public int[] hugepages { get; set; }
    }

    internal class hashrate
    {
        public double?[] total { get; set; }
        public double? highest { get; set; }
    }
}
