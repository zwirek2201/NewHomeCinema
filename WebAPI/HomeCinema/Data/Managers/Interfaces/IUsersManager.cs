using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public interface IUsersManager
    {
        Task AddUser(RegisterRequestInfo info);

        Task<LoginResponseInfo> AuthenticateUser(LoginRequestInfo info);

        Task<bool> CheckUserAuthentication(HttpRequest request);

        Task LogOut(string token);
    }
}
