using CryptoLocalBack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBase
{
    public class VideocardView
    {
        public string FullName { get; set; } = string.Empty;
        public ManufacturerType CCDType { get; set; } = ManufacturerType.UNKNOWN;
        public string CardManufacturer { get; set; } = string.Empty;
        public string CCDModel { get; set; } = string.Empty;
        public string GPUFrequency { get; set; } = string.Empty;
        public string MemoryFrequency { get; set; } = string.Empty;
        public MonitoringView MonitoringView { get; set; } = null!;

        public VideocardView(Videocard videocard, Monitoring mon)
        {
            FullName = videocard.FullName;
            CCDType = videocard.CCDType;
            CardManufacturer = videocard.CardManufacturer;
            CCDModel = videocard.CCDModel;
            GPUFrequency = videocard.GPUFrequency;
            MemoryFrequency = videocard.MemoryFrequency;
            MonitoringView = new MonitoringView(mon);
        }

    }
}
