using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeCinema.Data;
using System.Net;

namespace HomeCinema.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        // GET: api/Categories
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Category> categories = CategoriesManager.GetCategories();

                return Ok(categories);
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategories")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    Category category = CategoriesManager.GetCategory(id);

                    return Ok(category);
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

        // GET: api/Categories/5
        [HttpGet("{id}/movies", Name = "GetMovieCategories")]
        public IActionResult GetCategoryMovies(int id)
        {
            try
            {
                if (id > 0)
                {
                    List<Movie> movies = MoviesManager.GetMoviesByCategory(id);

                    return Ok(movies);
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

        // POST: api/Categories
        [HttpPost]
        public IActionResult Post([FromBody]Category value)
        {
            try
            {
                if (value != null)
                {
                    Category category = CategoriesManager.AddCategory(value);

                    return Ok(category);
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
        
        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    CategoriesManager.DeleteCategory(id);

                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch(NotFoundException ex)
            {
                return NotFound();
            }
            catch(CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
