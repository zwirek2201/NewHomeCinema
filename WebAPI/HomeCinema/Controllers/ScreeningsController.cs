using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeCinema.Data;

namespace HomeCinema.Controllers
{
    [Produces("application/json")]
    [Route("api/Screenings")]
    public class ScreeningsController : Controller
    {
        private IScreeningsManager _screeningsManager;

        public ScreeningsController(IScreeningsManager screeningsManager)
        {
            _screeningsManager = screeningsManager;
        }

        // GET: api/Screenings
        [HttpGet]
        public async Task<IActionResult> GetScreenings()
        {
            List<Screening> screenings = await _screeningsManager.GetScreenings();

            return Ok(screenings);
        }
    }
}
