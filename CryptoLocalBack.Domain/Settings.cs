using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocalBack.Domain
{
    [Table("Settings")]
    public class Settings
    {
        [Key] public Guid Id { get; set; }

        public string WalletName { get; set; } = string.Empty;
        public string WalletCoinName { get; set; } = string.Empty;
        public string WalletAddress { get; set; } = string.Empty;

        public string RigName { get; set; } = string.Empty;
        public string ServerName { get; set; } = string.Empty;

    }
}
