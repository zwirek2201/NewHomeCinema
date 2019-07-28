using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class Screening
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public string AudioType { get; set; }
        public string VideoType { get; set; }
    }
}
