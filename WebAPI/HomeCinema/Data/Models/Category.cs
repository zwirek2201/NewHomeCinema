using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class Category : DataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public override void FillFromReader(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            Name = reader.GetString(1);
            Description = reader.GetString(2);
            ThumbnailUrl = reader.GetString(3);
        }
    }
}
