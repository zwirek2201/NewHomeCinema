using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class UsersManager : IUsersManager
    {
        const string countUsersByEmailQuery = "Select count(*) from Users where Email = @Email";
        const string getUserByEmailQuery = "Select * from Users where Email = @Email";
        const string addUserQuery = "insert into Users output inserted.Id values(@FirstName, @LastName, @Email, @PasswordHash)";
        const string getUserAuthenticationInfoByEmail = "select * from UserAuthentication t1 join Users t2 on t1.UserId = t2.Id where t2.Email = @Email";
        const string getUserAuthenticationInfoByToken = "select * from UserAuthentication where Token = @Token";
        const string addUserAuthenticationInfo = "insert into UserAuthentication values(@UserId, @Token, @Created, @Expires)";
        const string deleteUserAuthenticationInfoByToken = "delete from UserAuthentication where Token = @Token";

        private IConfiguration _config;

        public UsersManager(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("HomeCinema"));
            }
        }

        public async Task AddUser(RegisterRequestInfo info)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                User user = await GetUserByEmail(info.Email);

                if (user == null)
                {
                    await conn.ExecuteAsync(addUserQuery, new { FirstName = info.FirstName, LastName = info.LastName, Email = info.Email, PasswordHash = info.PasswordHash });
                }
                else
                {
                    throw new UserAlreadyRegisteredException();
                }
            }
        }

        public async Task<LoginResponseInfo> AuthenticateUser(LoginRequestInfo info)
        {
            User user = await GetUserByEmail(info.Email);

            if (user != null)
            {
                if (user.PasswordHash == info.PasswordHash)
                {
                    LoginResponseInfo loginInfo = await GetAuthenticationByEmail(user.Email);

                    if (loginInfo != null)
                    {
                        if (loginInfo.Expires > DateTime.Now)
                        {
                            return loginInfo;
                        }
                        else
                        {
                            await RemoveAuthenticationByToken(loginInfo.Token);

                            LoginResponseInfo responseInfo = await CreateAuthentication(user.Id);
                            return responseInfo;
                        }
                    }
                    else
                    {
                        LoginResponseInfo responseInfo = await CreateAuthentication(user.Id);

                        return responseInfo;
                    }
                }
                else
                {
                    throw new BadLoginDataException();
                }
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public async Task<bool> CheckUserAuthentication(HttpRequest request)
        {
            if (request.Headers.ContainsKey("access-token"))
            {
                string token = request.Headers["access-token"];

                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    LoginResponseInfo info = await GetAuthenticationByToken(token);

                    if(info != null)
                    {
                        if(info.Expires > DateTime.Now)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        private async Task<LoginResponseInfo> GetAuthenticationByToken(string token)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                return await conn.QuerySingleOrDefaultAsync<LoginResponseInfo>(getUserAuthenticationInfoByToken, new { Token = token });
            }
        }

        private async Task<LoginResponseInfo> GetAuthenticationByEmail(string email)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                return await conn.QuerySingleOrDefaultAsync<LoginResponseInfo>(getUserAuthenticationInfoByEmail, new { Email = email });
            }
        }

        private async Task RemoveAuthenticationByToken(string token)
        {
            using(IDbConnection conn = Connection)
            {
                conn.Open();

                await conn.ExecuteAsync(deleteUserAuthenticationInfoByToken, new { Token = token });
            }
        }

        private async Task<LoginResponseInfo> CreateAuthentication(int userId)
        {

            string token = Guid.NewGuid().ToString();
            DateTime created = DateTime.Now;
            DateTime expires = DateTime.Now.AddDays(1);

            using (IDbConnection conn = Connection)
            {
                conn.Open();

                await conn.ExecuteAsync(addUserAuthenticationInfo, new { UserId = userId, Token = token, Created = created, Expires = expires });

                LoginResponseInfo responseInfo = new LoginResponseInfo()
                {
                    UserId = userId,
                    Token = token,
                    Created = created,
                    Expires = expires
                };

                return responseInfo;
            }
        }

        public async Task LogOut(string token)
        {
            await RemoveAuthenticationByToken(token);
        }

        private async Task<User> GetUserByEmail(string email)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                var result = await conn.QueryAsync<User>(getUserByEmailQuery, new { Email = email });

                return result.FirstOrDefault();
            }
        }
    }
}
