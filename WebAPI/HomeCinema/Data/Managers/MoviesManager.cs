using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class MoviesManager : IMoviesManager
    {
        private const string getMoviesQuery = "Select * from Movies";
        private const string addMovieQuery = "Insert into Movies output INSERTED.Id values(@Title, @Subtitle, @Desc, @Category, @Url)";
        private const string getMovieByIdQuery = "Select * from Movies where Id=@Id";
        private const string getMoviesByCategoryQuery = "Select * from Movies where CategoryId=@Id";
        private const string removeMovieByIdQuery = "Delete from Movies where Id=@Id";
        private const string getMoviesCountQuery = "select count(*) from Movies";

        private readonly IConfiguration _config;

        public MoviesManager(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("HomeCinema"));
            }
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                var result = await conn.QuerySingleAsync<int>(addMovieQuery, new { TItle = movie.Title, Subtitle = movie.SubTitle, Desc = movie.Description, Category = movie.CategoryId, Url = movie.PosterUrl });

                movie.Id = result;

                return movie;
            }
        }

        public async Task DeleteMovie(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                await conn.ExecuteAsync(removeMovieByIdQuery, new { Id = id });
            }
        }

        public async Task<Movie> GetMovieById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.QueryAsync<Movie>(getMovieByIdQuery, new { Id = id});

                return result.FirstOrDefault();
            }
        }

        public async Task<List<Movie>> GetMovies(int skip = 0, int limit = 0)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.QueryAsync<Movie>(getMoviesQuery);

                if(skip >= 0 && limit > 0)
                {
                    result = result.Skip(skip).Take(limit);
                }

                return result.ToList();
            }
        }

        public async Task<List<Movie>> GetMoviesByCategory(int categoryId)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.QueryAsync<Movie>(getMoviesByCategoryQuery, new { Id = categoryId});

                return result.ToList();
            }
        }

        public async Task<int> GetMoviesCount()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.QuerySingleAsync<int>(getMoviesCountQuery);

                return result;
            }
        }
    }
}
