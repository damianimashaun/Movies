using Microsoft.AspNetCore.Mvc;
using MoviesApi.Interfaces;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private IReaderService ReaderService;

        public MoviesController(IReaderService readerService)
        {
            ReaderService = readerService;
        }

        [HttpGet]
        [Route("stats")]
        public IActionResult Stats()
        {
            var stats = ReaderService.MakeStats();
            return Ok(stats);
        }
    }
}
