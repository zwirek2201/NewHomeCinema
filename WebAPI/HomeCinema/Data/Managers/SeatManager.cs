using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class SeatManager
    {
        private const string getRoomSeats = "Select * from Seats where RoomId = @RoomId";

        public static List<Seat> GetRoomSeats(int roomId)
        {
            DbReader reader = DbReader.GetInstance();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("RoomId", roomId));

            List<Seat> seats = new List<Seat>();

            try
            {
                seats = reader.RunReader<Seat>(getRoomSeats, parameters);

                if (seats == null)
                    seats = new List<Seat>();

                return seats;
            }
            catch (Exception ex)
            {
                throw new CouldNotPerformOperationException() { InnerException = ex };
            }

        }
    }
}
