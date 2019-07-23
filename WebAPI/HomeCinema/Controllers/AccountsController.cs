using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCinema.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCinema.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        // GET: api/Authorization/5
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterRequestInfo info)
        {
            try
            {
                UserManager.RegisterUser(info);

                return Ok();
            }
            catch(UserAlreadyRegisteredException ex)
            {
                return BadRequest("User already exists");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginRequestInfo info)
        {
            LoginResponseInfo response = UserManager.AuthenticateUser(info);

            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (Request.Headers.ContainsKey("access-token"))
            {
                string token = Request.Headers["access-token"];
                UserManager.LogOut(token);

                return Ok();
            }
            else
            {
                return BadRequest("Access denied!");
            }
        }
    }
}
