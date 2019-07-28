using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class LoginResponseInfo
    {
        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }
    }
}
