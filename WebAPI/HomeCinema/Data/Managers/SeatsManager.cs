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
    public class SeatsManager : ISeatsManager
    {
        private const string getRoomSeats = "Select * from Seats where RoomId = @RoomId";

        private IConfiguration _config;

        public SeatsManager(IConfiguration config)
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

        public async Task<List<Seat>> GetRoomSeats(int roomId)
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Seat>(getRoomSeats, new { RoomId = roomId });

                return result.ToList();
            }
        }
    }
}
