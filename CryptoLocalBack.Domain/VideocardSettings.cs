using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocalBack.Domain
{
    [Table("VideocardSettings")]
    public class VideocardSettings
    {
        [Key] public Guid Id { get; set; }  
        public double PowerLimit { get; set; }
        public double CoreLimit { get; set; }
        public double MemoryLimit { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
