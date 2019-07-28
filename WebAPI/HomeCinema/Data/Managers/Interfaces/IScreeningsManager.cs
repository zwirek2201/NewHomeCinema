using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public interface IScreeningsManager
    {
        Task<List<Screening>> GetScreenings();
    }
}
