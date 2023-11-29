using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocalBack.Domain
{
    [Table("Videocard")]
    public class Videocard
    {
        [Key] public Guid Id { get; set; }
        
        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Производитель видеочипа
        /// </summary>
        public ManufacturerType CCDType { get; set; } = ManufacturerType.UNKNOWN;

        /// <summary>
        /// Производитель всей карты
        /// </summary>
        public string CardManufacturer { get; set; } = string.Empty;

        /// <summary>
        /// Модель видеочипа
        /// </summary>
        public string CCDModel { get; set; } = string.Empty;

        /// <summary>
        /// Частота ядра
        /// </summary>
        public string GPUFrequency { get; set; } = string.Empty;

        /// <summary>
        /// Частота памяти
        /// </summary>
        public string MemoryFrequency { get; set; } = string.Empty;

        public List<Monitoring> Monitorings { get; set; } = new();

    }

    public enum ManufacturerType
    {
        UNKNOWN,
        AMD,
        NVIDIA
    }
}
