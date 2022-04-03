using Microsoft.AspNetCore.Mvc;

namespace MovieAppMLModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;

        //TODO: Add the prediction engine model to the controller class through dependency injection
        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }

        //TODO: Implement a controller action that will return a json string which contains movie id's recomended by the predicion
        //engine for a provided user Id
        [HttpGet(Name = "GetRecommendation")]
        public string Get()
        {
            string placeHolder = "Dummy data, controler not implemented yet";
            return placeHolder;
        }
    }
}