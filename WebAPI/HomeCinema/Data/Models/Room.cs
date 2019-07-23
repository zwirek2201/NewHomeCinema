using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class Room : DataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public override void FillFromReader(SqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"]);
            Name = reader["Name"].ToString();
            Description = reader["Description"].ToString();
        }
    }
}
