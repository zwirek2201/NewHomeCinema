using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCinema.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeCinema.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (UserManager.CheckUserAuthentication(Request))
                {
                    return Ok(new string[] { "value1", "value2" });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (NotAuthenticatedException)
            {
                return Unauthorized();
            }
            catch (TokenExpiredException)
            {
                return Unauthorized();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
