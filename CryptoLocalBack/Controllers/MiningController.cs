using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoLocalBack.Domain;
using CryptoLocalBack.Extensions;
using CryptoLocalBack.Infrastructure;
using CryptoLocalBack.Model.Views;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics;
using CryptoLocalBack.Model.Models;
using CryptoLocalBack.Helpers;

namespace CryptoLocalBack.Controllers
{
    public class MiningController : Controller
    {
        private readonly ILogger _logger;

        public MiningController(ILogger<MiningController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/miner/{minerName}/start")]
        public async Task<ActionResult<DockerAnswerView>> StartMiner(
            [FromServices] SystemctlExtension docker,
            [FromRoute] string minerName,
            [FromBody] MinerStartModel model)
        {
            if (string.IsNullOrWhiteSpace(minerName)
                || string.IsNullOrWhiteSpace(model.WalletName)
                || string.IsNullOrWhiteSpace(model.PoolAddress))
            {
                return BadRequest("Bad params");
            }

            MinerType minerType = SystemctlExtension.GetMinerTyme(minerName);
            if(minerType == MinerType.None)
            {
                return BadRequest("Cant find miner type. Please try myxmrig");
            }
            return Ok(await docker.StartContainer(minerType, model));
        }

        [HttpGet("/miner/{minerName}/restart")]
        public async Task<ActionResult<DockerAnswerView>> RestartMiner(
            [FromServices] SystemctlExtension docker,
            [FromRoute] string minerName)
        {
            MinerType minerType = SystemctlExtension.GetMinerTyme(minerName);
            if (minerType == MinerType.None)
            {
                return BadRequest("Cant find miner type. Please try myxmrig");
            }

            return Ok(await docker.RestartContainer(minerType));
        }


        [HttpGet("/miner/{minerName}/stop")]
        public async Task<ActionResult<DockerAnswerView>> StopMiner(
            [FromServices] SystemctlExtension docker,
            [FromRoute] string minerName)
        {
            MinerType minerType = SystemctlExtension.GetMinerTyme(minerName);
            if (minerType == MinerType.None)
            {
                return BadRequest("Cant find miner type. Please try myxmrig");
            }

            return Ok(await docker.StopContainer(minerType));
        }
    }
}
