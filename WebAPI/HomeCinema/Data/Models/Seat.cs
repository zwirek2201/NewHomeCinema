using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class Seat
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int TypeId { get; set; }
    }
}
