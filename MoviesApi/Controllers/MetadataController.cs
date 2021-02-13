using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Interfaces;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetadataController : ControllerBase
    {
        private IDatabaseProvider DatabaseProvider;

        public MetadataController(IDatabaseProvider databaseProvider)
        {
            DatabaseProvider = databaseProvider;
        }

        [HttpPost]
        public IActionResult Index(Movie movie)
        {
            DatabaseProvider.SaveMovie(movie);
            return Ok("Movie saved ");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Index(int id)
        {
            var movies = DatabaseProvider.GetMovies(id);

            if (movies.Count() < 1)
            {
                return NotFound();
            }

            return Ok(movies);
        }
    }
}
