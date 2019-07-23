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
        // GET: api/Movies
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Movie> movies = MoviesManager.GetMovies();

                return Ok(movies);
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "GetMovies")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    Movie movie = MoviesManager.GetMovie(id);

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

        // POST: api/Movies
        [HttpPost]
        public IActionResult Post([FromBody]Movie value)
        {
            try
            {
                if (value != null)
                {
                    Movie movie = MoviesManager.AddMovie(value);

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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    MoviesManager.DeleteMovie(id);

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
