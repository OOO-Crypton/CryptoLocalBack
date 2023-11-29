using CryptoLocalBack.Model.Models;
using CryptoLocalBack.Model.Views;
using CryptoLocalBack.Helpers;

namespace CryptoLocalBack.Extensions
{
    public enum MinerType
    {
        Phoenix,
        TeamRed,
        GMiner,
        Xmrig,
        None
    }
    public class SystemctlExtension
    {
        readonly IConfiguration _configuration;
        public SystemctlExtension(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task<DockerAnswerView> StartContainer(MinerType type, MinerStartModel model)
        {
            //await StartCommand("sudo systemctl", $"stop {str}");
            Console.WriteLine("PATH: " + GetPath(type));
            Console.WriteLine("DATA: " + GetLine(type, model));
            File.WriteAllText(GetPath(type), GetLine(type, model));
            return await RestartContainer(type);
        }

        public async Task<DockerAnswerView> RestartContainer(MinerType type)
        {
            return await Helpers.Helpers.StartCommand("sudo", $"systemctl restart {GetContainerName(type)}");
        }

        public async Task<DockerAnswerView> StopContainer(MinerType type)
        {
            return await Helpers.Helpers.StartCommand("sudo", $"systemctl stop {GetContainerName(type)}");
        }

        public string GetLine(MinerType type, MinerStartModel model)
        {
            string header = "#!/bin/bash\n";
            return type switch
            {
                MinerType.Phoenix => header + $"./parser/PhoenixParser & ./PhoenixMiner_6.2c_Linux/PhoenixMiner -pool {model.PoolAddress} -wal {model.WalletName} -worker rig1 {model.AdditionalParams} -cdmport 3333 > /dev/null 2>&1",
                MinerType.TeamRed => "/home/crypton/phoenix-image/aaa/minerStart.sh",
                MinerType.GMiner => "/home/crypton/phoenix-image/bbb/minerStart.sh",
                MinerType.Xmrig => "/home/crypton/phoenix-image/ccc/minerStart.sh",
                _ => "/",
            };
        }

        public string GetPath(MinerType type)
        {
            return type switch
            {
                MinerType.Phoenix => _configuration.GetSection("PathToPhoenixMinerStartScript").Value ?? "err",
                MinerType.TeamRed => "/home/crypton/phoenix-image/aaa/minerStart.sh",
                MinerType.GMiner => "/home/crypton/phoenix-image/bbb/minerStart.sh",
                MinerType.Xmrig => "/home/crypton/phoenix-image/ccc/minerStart.sh",
                _ => "/",
            };
        }

        public static string GetContainerName(MinerType type)
        {
            return type switch
            {
                MinerType.Phoenix => "phoenixMiner.service",
                MinerType.TeamRed => "teamredMiner.service",
                MinerType.GMiner => "gMiner.service",
                MinerType.Xmrig => "myxmrig.service",
                _ => "error",
            };
        }

        public static MinerType GetMinerTyme(string name)
        {
            return name switch
            {
                "phoenixMiner.service" => MinerType.Phoenix,
                "teamredMiner.service" => MinerType.TeamRed,
                "gMiner.service" => MinerType.GMiner,
                "myxmrig.service" => MinerType.Xmrig,
                _ => MinerType.None,
            };
        }
    }
}
