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
    public class RoomsManager : IRoomsManager
    {
        private const string getRoomsQuery = "Select * from Rooms";
        private const string getRoomByIdQuery = "Select * from Rooms where Id=@Id";

        private IConfiguration _config;

        public RoomsManager(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("HomeCinema"));
            }
        }

        public async Task<Room> GetRoom(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                var result = await conn.QueryAsync<Room>(getRoomByIdQuery, new { Id = id });

                return result.FirstOrDefault();
            }
        }

        public async Task<List<Room>> GetRooms()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                var result = await conn.QueryAsync<Room>(getRoomsQuery);

                return result.ToList();
            }
        }
    }
}
