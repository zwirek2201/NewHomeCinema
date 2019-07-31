using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class RepertoirManager : IRepertoirManager
    {
        private IConfiguration _config;
        private IScreeningsManager _screeningsManager;

        public RepertoirManager(IConfiguration config, IScreeningsManager screeningsManager)
        {
            _config = config;
            _screeningsManager = screeningsManager;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("HomeCinema"));
            }
        }

        public async Task<DayRepertoir> GetDayRepertoir(DateTime date)
        {
            List<Screening> screenings = await _screeningsManager.GetDayScrenings(date);

            return new DayRepertoir();
        }
    }
}
