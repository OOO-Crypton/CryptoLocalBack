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


namespace CryptoLocalBack.Controllers
{
    public class DataController : Controller
    {
        private readonly ILogger _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }


        [HttpGet("/monitoring/list")]
        public async Task<ActionResult<List<MonitoringView>>> GetMonitorings(
            [FromServices] CryptoLocalBackDbContext db,
            CancellationToken ct)
        {
            DateTime dateTime = DateTime.Now.AddDays(-1);
            return Ok(db.Monitorings
                    .Include(x => x.Videocard)
                    .AsEnumerable()
                    .Where(x => dateTime.Ticks < x.Date.Ticks)
                    .Select(x => new MonitoringView(x))
                    .ToList()
                   );
        }




    }
}
