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
    public class VideocardController : Controller
    {
        private readonly ILogger _logger;

        public VideocardController(ILogger<DataController> logger)
        {
            _logger = logger;
        }


        [HttpPost("/overclocking/settings")]
        public async Task<ActionResult<DockerAnswerView>> PostOverclockingSettings(
            [FromServices] VideocardExtension vid,
            [FromBody] OverclockingModel overclockingModel,
            CancellationToken ct)
        {
            var res = await vid.ChangeSettings(overclockingModel);
            if (res.ExitCode != 0) return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("/overclocking/getCurrentSettings")]
        public async Task<ActionResult<VideocardSettings>> GetCurrentSettings(
            [FromServices] VideocardExtension vid,
            [FromBody] OverclockingModel overclockingModel,
            CancellationToken ct)
        {
            return await vid.GetCurrentSettings();
        }
    }
}
