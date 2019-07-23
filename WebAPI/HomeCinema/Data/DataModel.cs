using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public abstract class DataModel
    {
        public abstract void FillFromReader(SqlDataReader reader);
    }
}
