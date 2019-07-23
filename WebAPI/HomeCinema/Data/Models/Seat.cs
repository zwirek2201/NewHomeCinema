using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class Seat : DataModel
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int TypeId { get; set; }

        public override void FillFromReader(SqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            RoomId = Convert.ToInt32(reader["RoomId"]);
            Row = Convert.ToInt32(reader["Row"]);
            Column = Convert.ToInt32(reader["Column"]);
            TypeId = Convert.ToInt32(reader["TypeId"]);
        }
    }
}
