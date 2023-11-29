using CryptoLocalBack.Domain;
using System.ComponentModel.DataAnnotations;

namespace CryptoLocalBack.Model.Views
{
    public class VideocardView
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public ManufacturerType CCDType { get; set; }
        public string CardManufacturer { get; set; }
        public string CCDModel { get; set; }
        public string GPUFrequency { get; set; }
        public string MemoryFrequency { get; set; }

        public VideocardView(Videocard videocard)
        {
            Id = videocard.Id;
            FullName = videocard.FullName;
            CCDType = videocard.CCDType;
            CardManufacturer = videocard.CardManufacturer;
            CCDModel = videocard.CCDModel;
            GPUFrequency = videocard.GPUFrequency;
            MemoryFrequency = videocard.MemoryFrequency;
        }
    }
}
