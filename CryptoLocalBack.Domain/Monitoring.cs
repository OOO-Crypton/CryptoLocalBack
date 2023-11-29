using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocalBack.Domain
{
    [Table("Monitorings")]
    public class Monitoring
    {
        [Key] public Guid Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Текущий хэшрейт
        /// </summary>
        public double CurrentHashrate { get; set; }

        /// <summary>
        /// Температура GPU
        /// </summary>
        public double GPUTemperature { get; set; }

        /// <summary>
        /// Скорость вентилятора
        /// </summary>
        public double FanRPM { get; set; }

        /// <summary>
        /// Потребление каждой карты
        /// </summary>
        public double EnergyConsumption { get; set; }

        public Videocard Videocard { get; set; } = null!;

        public override string ToString()
        {
            return $"CurrentHashrate: {CurrentHashrate},\nGPUTemperature: {GPUTemperature}," +
                $"\nFanRPM: {FanRPM},\nEnergyConsumption: {EnergyConsumption},\nVideocard: {Videocard.FullName}";
        }
    }
}
