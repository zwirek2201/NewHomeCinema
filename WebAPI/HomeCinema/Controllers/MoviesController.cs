using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCinema.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCinema.Controllers
{
    [Produces("application/json")]
    [Route("api/Movies")]
    public class MoviesController : Controller
    {
        private IMoviesManager _moviesManager;

        public MoviesController(IMoviesManager moviesManager)
        {
            _moviesManager = moviesManager;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IActionResult> Get(int skip = 0, int limit = 0)
        {
            try
            {
                List<Movie> movies = await _moviesManager.GetMovies(skip, limit);

                return Ok(movies);
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Movie movie = await _moviesManager.GetMovieById(id);

                    return Ok(movie);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("count", Name = "GetMoviesCount")]
        public async Task<IActionResult> GetCount()
        {
            try
            {

                    int movieCount = await _moviesManager.GetMoviesCount();

                    return Ok(movieCount);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Movie value)
        {
            try
            {
                if (value != null)
                {
                    Movie movie = await _moviesManager.AddMovie(value);

                    return Ok(movie);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string value)
        {
            return NotFound();
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    await _moviesManager.DeleteMovie(id);

                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
