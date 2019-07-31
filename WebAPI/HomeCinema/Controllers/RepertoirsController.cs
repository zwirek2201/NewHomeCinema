using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCinema.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HomeCinema.Controllers
{
    [Produces("application/json")]
    [Route("api/Repertoirs")]
    public class RepertoirsController : Controller
    {
        public IRepertoirManager _repertoirManager;

        public RepertoirsController(IRepertoirManager repertoirManager)
        {
            _repertoirManager = repertoirManager;
        }

        // GET: api/Repertoirs
        [HttpGet("{date}", Name = "GetDayRepertoirs")]
        public async Task<IActionResult> GetDayRepertoir(string date)
        {
            DateTime repertoirDate;

            if (DateTime.TryParseExact(date, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out repertoirDate))
            {
                var result = await _repertoirManager.GetDayRepertoir(repertoirDate);
                return Ok(result);
            }
            else
            {
                return BadRequest("Repertoir date format is incorrect.");
            }
        }
        
        // POST: api/Repertoirs
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Repertoirs/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
