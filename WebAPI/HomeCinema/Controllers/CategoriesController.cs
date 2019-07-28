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
        private readonly ICategoriesManager _categoriesManager;
        private readonly IMoviesManager _moviesManager;

        public CategoriesController(ICategoriesManager categoriesManager, IMoviesManager moviesManager)
        {
            _categoriesManager = categoriesManager;
            _moviesManager = moviesManager;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Category> categories = await _categoriesManager.GetCategories();

                return Ok(categories);
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategories")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    Category category = await _categoriesManager.GetCategory(id);

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
        public async Task<IActionResult> GetCategoryMovies(int id)
        {
            try
            {
                if (id > 0)
                {
                    List<Movie> movies = await _moviesManager.GetMoviesByCategory(id);

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
        public async Task<IActionResult> Post([FromBody]Category value)
        {
            try
            {
                if (value != null)
                {
                    Category category = await _categoriesManager.AddCategory(value);

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    await _categoriesManager.DeleteCategory(id);

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
