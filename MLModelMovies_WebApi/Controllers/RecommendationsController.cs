using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using MLModelMovies_WebApi.Models;
using MLModelMovies_WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLModelMovies_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly ILogger<RecommendationsController> _logger;

        public RecommendationsController(ILogger<RecommendationsController> logger)
        {
            _logger = logger;
        }


        [HttpPost("predictions/{numberOfPredictions:long}", Name = "multipleMoviePredictions")]
        public async Task<ActionResult<IEnumerable<long>>> GetRecommendations([FromServices] PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, [FromRoute]int numberOfPredictions, [FromBody]IEnumerable<ModelInput> ratedMovies)
        {
            //test code for the api endpoint, to be removed
            List<ModelInput> rated = new(ratedMovies);
            try
            {
                if (numberOfPredictions < 1) throw new ArgumentException("The request needs to be made for at least on predition");
                InputValidator.ValidateUserRatings(rated);
            }
            catch (ArgumentException ex) {
                _logger.LogError(ex.Message);
                return StatusCode(422);
            }
            
            return new List<long> { numberOfPredictions, rated.Count };
        }

        [HttpPost("singlePrediction", Name = "singlePrediction")]
        public async Task<ModelOutput> GetOnePrediction([FromServices]PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, [FromBody]ModelInput input)
        {
            var predResult = await Task.FromResult(predictionEnginePool.Predict(input));
            return predResult;
        }
    }
}
