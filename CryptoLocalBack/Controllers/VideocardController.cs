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
            [FromServices] CryptoLocalBackDbContext db,
            [FromBody] OverclockingModel overclockingModel,
            CancellationToken ct)
        {

            VideocardExtension vid = new(db);
            await vid.ChangeSettings(overclockingModel);
            return Ok();
        }

        [HttpGet("/overclocking/getCurrentSettings")]
        public async Task<ActionResult<VideocardSettings>> GetCurrentSettings(
            [FromServices] CryptoLocalBackDbContext db,
            CancellationToken ct)
        {
            return Ok(await db.VideocardSettings.ToListAsync(ct));
        }
    }
}
