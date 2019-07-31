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
        private IMoviesManager _moviesManager;

        public RepertoirManager(IConfiguration config, IScreeningsManager screeningsManager, IMoviesManager moviesManager)
        {
            _config = config;
            _screeningsManager = screeningsManager;
            _moviesManager = moviesManager;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("HomeCinema"));
            }
        }

        public async Task<List<MovieScreeningList>> GetDayRepertoir(DateTime date)
        {
            try
            {
                List<MovieScreeningList> repertoir = new List<MovieScreeningList>();

                List<Screening> screenings = await _screeningsManager.GetDayScrenings(date);
                List<Movie> movies = await _moviesManager.GetMovies();

                if (screenings.Any())
                {
                    foreach (Screening screening in screenings)
                    {
                        MovieScreeningList movieScreeningList = repertoir.FirstOrDefault(obj => obj.Movie.Id == screening.MovieId && obj.AudioType == screening.AudioType && obj.VideoType == screening.VideoType);

                        if (movieScreeningList != null)
                        {
                            movieScreeningList.Screenings.Add(screening);
                        }
                        else
                        {
                            movieScreeningList = new MovieScreeningList()
                            {
                                Movie = movies.FirstOrDefault(m => m.Id == screening.MovieId),
                                AudioType = screening.AudioType,
                                VideoType = screening.VideoType,
                                Screenings = new List<Screening>() { screening }
                            };

                            repertoir.Add(movieScreeningList);
                        }
                    }
                }

                return repertoir;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
