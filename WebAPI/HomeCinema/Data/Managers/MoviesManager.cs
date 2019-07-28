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

        public async Task<List<Movie>> GetMovies(int skip, int limit)
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

        //public static List<Movie> GetMovies()
        //{
        //    try
        //    {
        //        DbReader reader = DbReader.GetInstance();

        //        List<Movie> movies = reader.RunReader<Movie>(getMoviesQuery);

        //        return movies;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CouldNotPerformOperationException() { InnerException = ex };
        //    }
        //}

        //public static List<Movie> GetMoviesByCategory(int categoryId)
        //{
        //    try
        //    {
        //        DbReader reader = DbReader.GetInstance();

        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("Id", categoryId));

        //        List<Movie> movies = reader.RunReader<Movie>(getMoviesByCategoryQuery, parameters);

        //        return movies;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CouldNotPerformOperationException() { InnerException = ex };
        //    }
        //}

        //public static Movie GetMovie(int id)
        //{

        //    DbReader reader = DbReader.GetInstance();

        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("Id", id));

        //    List<Movie> movies = new List<Movie>();

        //    try
        //    {
        //        movies = reader.RunReader<Movie>(getMovieByIdQuery, parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CouldNotPerformOperationException() { InnerException = ex };
        //    }

        //    if (movies.Any())
        //    {
        //        return movies.FirstOrDefault();
        //    }
        //    else
        //    {
        //        throw new NotFoundException();
        //    }

        //}

        //public static Movie AddMovie(Movie movie)
        //{
        //    try
        //    {
        //        DbReader reader = DbReader.GetInstance();

        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("Title", movie.Title));
        //        parameters.Add(new SqlParameter("SubTitle", movie.SubTitle));
        //        parameters.Add(new SqlParameter("Desc", movie.Description));
        //        parameters.Add(new SqlParameter("Category", movie.CategoryId));
        //        parameters.Add(new SqlParameter("Url", movie.PosterUrl));

        //        int id = reader.RunSingleReader<int>(addMovieQuery, parameters);

        //        if (id > 0)
        //        {
        //            movie.Id = id;
        //            return movie;
        //        }
        //        else
        //        {
        //            throw new CouldNotPerformOperationException();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CouldNotPerformOperationException() { InnerException = ex };
        //    }
        //}

        //public static void DeleteMovie(int id)
        //{
        //    DbReader reader = DbReader.GetInstance();

        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("Id", id));

        //    List<Movie> movies = reader.RunReader<Movie>(getMovieByIdQuery, parameters);

        //    if (movies.Any())
        //    {
        //        int affectedRows;
        //        try
        //        {
        //            parameters = new List<SqlParameter>();
        //            parameters.Add(new SqlParameter("Id", id));

        //            affectedRows = reader.RunSimpleQuery(getMovieByIdQuery, parameters);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new CouldNotPerformOperationException() { InnerException = ex };
        //        }

        //        if (affectedRows == 1)
        //        {

        //        }
        //        else
        //        {
        //            throw new CouldNotPerformOperationException();
        //        }
        //    }
        //    else
        //    {
        //        throw new NotFoundException();
        //    }
        //}
    }
}
