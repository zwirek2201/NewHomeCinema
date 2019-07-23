using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class Movie : DataModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string PosterUrl { get; set; }

        public override void FillFromReader(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            Title = reader.GetString(1);
            SubTitle = reader.GetString(2);
            Description = reader.GetString(3);
            CategoryId = reader.GetInt32(4);
            PosterUrl = reader.GetString(5);
        }
    }
}
