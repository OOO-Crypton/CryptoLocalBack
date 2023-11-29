using CryptoLocalBack.Domain;
using System.ComponentModel.DataAnnotations;

namespace CryptoLocalBack.Model.Views
{
    public class MonitoringView
    {
       public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public double CurrentHashrate { get; set; }
        public double GPUTemperature { get; set; }
        public double FanRPM { get; set; }
        public double EnergyConsumption { get; set; }

        public MonitoringView(Monitoring monitoring)
        {
            Id = monitoring.Id;
            Date = monitoring.Date;
            IsActive = monitoring.IsActive;
            CurrentHashrate = monitoring.CurrentHashrate;
            GPUTemperature = monitoring.GPUTemperature;
            FanRPM = monitoring.FanRPM;
            EnergyConsumption = monitoring.EnergyConsumption;
        }
    }
}
