using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class LoginResponseInfo : DataModel
    {
        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public override void FillFromReader(SqlDataReader reader)
        {
            UserId = Convert.ToInt32(reader["UserId"]);
            Token = reader["Token"].ToString();
            Created = DateTime.Parse(reader["Created"].ToString());
            Expires = DateTime.Parse(reader["Expires"].ToString());
        }
    }
}
