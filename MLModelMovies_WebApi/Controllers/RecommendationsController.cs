using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using MLModelMovies_WebApi.Models;
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
        public async Task<IEnumerable<long>> GetRecommendations([FromServices] PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, [FromRoute]int numberOfPredictions, [FromBody]IEnumerable<ModelInput> ratedMovies)
        {
            //test code for the api endpoint
            List<ModelInput> rated = new List<ModelInput>(ratedMovies);
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
