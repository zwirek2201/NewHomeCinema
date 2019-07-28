using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class ScreeningsManager : IScreeningsManager
    {
        private string getScreeningsQuery = "select * from Screenings";

        private IConfiguration _config;

        public ScreeningsManager(IConfiguration config)
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

        public async Task<List<Screening>> GetScreenings()
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Screening>(getScreeningsQuery);

                return result.ToList();
            }
        }
    }
}
