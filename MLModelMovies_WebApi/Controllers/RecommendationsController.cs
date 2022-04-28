using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using MLModelMovies_WebApi.Models;
using MLModelMovies_WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace MLModelMovies_WebApi.Controllers
{
    /// <summary>
    /// The controller used for the entire API, containing two endpoints, 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly ILogger<RecommendationsController> _logger;
        /// <summary>
        /// represents a PredictionEnginePool injected in the controller through the constructor
        /// </summary>
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        /// <summary>
        /// constructor for the controller
        /// </summary>
        /// <param name="logger">logging interface for logging errors</param>
        /// <param name="predictionEnginePool">PredictionEnginePool from which to extract prediction engines, 
        /// a prediction engine should be used for a single prediction
        /// </param>
        public RecommendationsController(ILogger<RecommendationsController> logger, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        /// <summary>
        /// The main endpoint of the API, accessed at https://localhost:61286/recommendations/predictions/numberOfPredictions
        /// </summary>
        /// <param name="numberOfPredictions">The number of predicitons that should be returned</param>
        /// <param name="ratedMovies">A list of movie ratings for a user</param>
        /// <returns>A list of numberOfPredictions movies recomended for the user to which the provided ratings belong</returns>
        [HttpPost("predictions/{numberOfPredictions:long}", Name = "multipleMoviePredictions")]
        public async Task<ActionResult<IEnumerable<float>>> GetRecommendations([FromRoute]int numberOfPredictions, [FromBody]IEnumerable<ModelInput> ratedMovies)
        {
            //input validation
            List<ModelInput> rated = new(ratedMovies);
            try
            {
                if (numberOfPredictions < 1) throw new ArgumentException("The request needs to be made for at least on predition");
                InputValidator.ValidateUserRatings(rated);
            }
            catch (ArgumentException ex) {
                _logger.LogError(ex.Message);
                return StatusCode(((int)HttpStatusCode.UnprocessableEntity));
            }

            //testing for the retraining of the model, subject to be removed
            //at the moment it just returns the predicted rating for the movies
            //with id 15 and 5, and works only if in the list of ratedMovies were included ratings for these movie ids
            var newEngine = MLModelMovies.RetrainingForNewUser(rated);

            var testMovies = new List<float>() { 15f, 5f };
            List<float> results = new List<float>();

            foreach(var movie in testMovies)
            {
                ModelOutput prediction = newEngine.Predict(new ModelInput { MovieId = movie, UserId = rated[0].UserId + 1f + Constants.trainingDataLastUser});
                results.Add(prediction.Score);
            }
            
            return results;
        }

        /// <summary>
        /// API endpoint used for testing the model https://localhost:61286/recommendations/singlePrediction
        /// </summary>
        /// <param name="input">Input for the machine learning model, consisting of a userId and movieId</param>
        /// <returns>Predicted rating from the user for the movie</returns>
        [HttpPost("singlePrediction", Name = "singlePrediction")]
        public async Task<ModelOutput> GetOnePrediction([FromBody]ModelInput input)
        {
            var predResult = await Task.FromResult(_predictionEnginePool.Predict("MovieRecommendationsModel", input));
            return predResult;
        }
    }
}
