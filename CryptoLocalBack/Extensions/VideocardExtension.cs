using CryptoLocalBack.Domain;
using CryptoLocalBack.Helpers;
using CryptoLocalBack.Infrastructure;
using CryptoLocalBack.Model.Models;
using MSI.Afterburner;
using MSI.Afterburner.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CryptoLocalBack.Extensions
{
    public class VideocardExtension
    {
        readonly CryptoLocalBackDbContext _db;
        public VideocardExtension(CryptoLocalBackDbContext db)
        {
            _db = db;
        }

        public async Task ChangeSettings(OverclockingModel model)
        {
            await Helpers.Helpers.StartCommand("sudo", $"nvidia-smi -pl {model.PowerLimit}");
            await Helpers.Helpers.StartCommand("sudo", $"nvidia-settings -c :0 -a [gpu:0]/GPUGraphicsClockOffset[2]={model.CoreLimit}");
            await Helpers.Helpers.StartCommand("sudo", $"nvidia-settings -c :0 -a [gpu:0]/GPUMemoryTransferRateOffset[2]={model.MemoryLimit}");
            await _db.VideocardSettings.AddAsync(new VideocardSettings()
            {
                CoreLimit = model.CoreLimit,
                CreateDate = DateTime.Now,
                MemoryLimit = model.MemoryLimit,
                PowerLimit = model.PowerLimit
            });
            await _db.SaveChangesAsync();
        }
    }
}
