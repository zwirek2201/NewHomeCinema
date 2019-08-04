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
        private IUsersManager _usersManager;

        public AccountsController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        // GET: api/Authorization/5
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestInfo info)
        {
            try
            {
                await _usersManager.AddUser(info);

                return Ok();
            }
            catch(UserAlreadyRegisteredException ex)
            {
                return BadRequest("User already exists");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestInfo info)
        {
            try
            {
                LoginResponseInfo response = await _usersManager.AuthenticateUser(info);

                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (Request.Headers.ContainsKey("access-token"))
            {
                string token = Request.Headers["access-token"];
                await _usersManager.LogOut(token);

                return Ok();
            }
            else
            {
                return BadRequest("Access denied!");
            }
        }
    }
}
