using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class DayRepertoir
    {
        public Movie Movie { get; set; }

        public DateTime Date { get; set; }

        public string AudioType { get; set; }

        public string VideoType { get; set; }

        public List<Screening> Screenings { get; set; }
    }
}
