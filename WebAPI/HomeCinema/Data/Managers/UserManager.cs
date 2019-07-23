using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class UserManager
    {
        const string countUsersByEmailQuery = "Select count(*) from Users where Email = @Email";
        const string getUserByEmailQuery = "Select * from Users where Email = @Email";
        const string addUserQuery = "insert into Users output inserted.Id values(@FirstName, @LastName, @Email, @PasswordHash)";
        const string getUserAuthenticationInfoByEmail = "select * from UserAuthentication t1 join Users t2 on t1.UserId = t2.Id where t2.Email = @Email";
        const string getUserAuthenticationInfoByToken = "select * from UserAuthentication where Token = @Token";
        const string addUserAuthenticationInfo = "insert into UserAuthentication values(@UserId, @Token, @Created, @Expires)";
        const string deleteUserAuthenticationInfoByToken = "delete from UserAuthentication where Token = @Token";

        public static void RegisterUser(RegisterRequestInfo info)
        {
            DbReader reader = DbReader.GetInstance();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Email", info.Email));

            int userCount = reader.RunSingleReader<int>(countUsersByEmailQuery, parameters);

            if (userCount == 0)
            {
                parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FirstName", info.FirstName));
                parameters.Add(new SqlParameter("LastName", info.LastName));
                parameters.Add(new SqlParameter("Email", info.Email));
                parameters.Add(new SqlParameter("PasswordHash", info.PasswordHash));

                int result = reader.RunSingleReader<int>(addUserQuery, parameters);

            }
            else
            {
                throw new UserAlreadyRegisteredException();
            }
        }

        public static LoginResponseInfo AuthenticateUser(LoginRequestInfo info)
        {
            User user = GetUserByEmail(info.Email);

            if (user != null)
            {
                if (user.Password == info.PasswordHash)
                {
                    LoginResponseInfo loginInfo = GetAuthenticationByEmail(user.Email);

                    if (loginInfo != null)
                    {
                        if(loginInfo.Expires > DateTime.Now)
                        {
                            return loginInfo;
                        }
                        else
                        {
                            RemoveAuthenticationByToken(loginInfo.Token);

                            LoginResponseInfo responseInfo = CreateAuthentication(user.Id);
                            return responseInfo;
                        }
                    }
                    else
                    {
                        LoginResponseInfo responseInfo = CreateAuthentication(user.Id);

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

        private static User GetUserByEmail(string email)
        {
            DbReader reader = DbReader.GetInstance();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Email", email));

            List<User> users = reader.RunReader<User>(getUserByEmailQuery, parameters);

            return users.Any() ? users.First() : null;
        }

        private static LoginResponseInfo GetAuthenticationByEmail(string email)
        {
            DbReader reader = DbReader.GetInstance();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Email", email));

            List<LoginResponseInfo> loginInfos = reader.RunReader<LoginResponseInfo>(getUserAuthenticationInfoByEmail, parameters);

            return loginInfos.Any() ? loginInfos.First() : null;
        }

        private static void RemoveAuthenticationByToken(string token)
        {
            DbReader reader = DbReader.GetInstance();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Token", token));

            reader.RunSimpleQuery(deleteUserAuthenticationInfoByToken, parameters);
        }

        private static LoginResponseInfo CreateAuthentication(int userId)
        {
            DbReader reader = DbReader.GetInstance();

            string token = Guid.NewGuid().ToString();
            DateTime created = DateTime.Now;
            DateTime expires = DateTime.Now.AddDays(1);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("UserId", userId));
            parameters.Add(new SqlParameter("Token", token));
            parameters.Add(new SqlParameter("Created", created));
            parameters.Add(new SqlParameter("Expires", expires));

            if (reader.RunSimpleQuery(addUserAuthenticationInfo, parameters) > 0)
            {
                LoginResponseInfo responseInfo = new LoginResponseInfo()
                {
                    UserId = userId,
                    Token = token,
                    Created = created,
                    Expires = expires
                };

                return responseInfo;
            }
            else
            {
                throw new CouldNotPerformOperationException();
            }
        }

        public static bool CheckUserAuthentication(HttpRequest request)
        {
            if (request.Headers.ContainsKey("access-token"))
            {
                string token = request.Headers["access-token"];

                DbReader reader = DbReader.GetInstance();

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Token", token));

                List<LoginResponseInfo> loginInfos = reader.RunReader<LoginResponseInfo>(getUserAuthenticationInfoByToken, parameters);

                if (loginInfos != null && loginInfos.Any())
                {
                    if (loginInfos.First().Expires > DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        throw new TokenExpiredException();
                    }
                }
                else
                {
                    throw new NotAuthenticatedException();
                }
            }
            else
            {
                throw new NotAuthenticatedException();
            }
        }

        public static void LogOut(string token)
        {
            RemoveAuthenticationByToken(token);
        }
    }
}
