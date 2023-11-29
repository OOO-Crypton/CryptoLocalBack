using CryptoLocalBack.Domain;
using System.ComponentModel.DataAnnotations;

namespace CryptoLocalBack.Model.Views
{
    public class SettingsView
    {
        public Guid Id { get; set; }

        public string WalletName { get; set; } = string.Empty;
        public string WalletCoinName { get; set; } = string.Empty;
        public string WalletAddress { get; set; } = string.Empty;

        public string RigName { get; set; } = string.Empty;
        public string ServerName { get; set; } = string.Empty;
        public SettingsView(Settings settings)
        {
            Id = settings.Id;
            WalletName = settings.WalletName;
            WalletCoinName = settings.WalletCoinName;
            WalletAddress = settings.WalletAddress;
            RigName = settings.RigName;
            ServerName = settings.ServerName;
        }
    }
}
