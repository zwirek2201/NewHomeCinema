using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public interface IMoviesManager
    {
        Task<List<Movie>> GetMovies(int skip, int limit);

        Task<List<Movie>> GetMoviesByCategory(int categoryId);

        Task<Movie> GetMovieById(int id);

        Task<Movie> AddMovie(Movie movie);

        Task DeleteMovie(int id);
    }
}
