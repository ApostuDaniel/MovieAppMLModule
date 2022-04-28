using MLModelMovies_WebApi.Models;
using System.Collections.Generic;

namespace MLModelMovies_WebApi.Utils
{
    /// <summary>
    /// Class used for storing utility methods
    /// </summary>
    public class UtilsClass
    {
        /// <summary>
        /// Adjust each users id by incremeting it with the value of the 
        /// last user in the training databae + 1(if the id's for the our application's actual users start at 0)
        /// </summary>
        /// <param name="ratings">List of user ratings</param>
        /// <returns>List of user ratings with adjusted id's</returns>
        public static List<ModelInput> AdjustUserId(List<ModelInput> ratings)
        {
            List<ModelInput> adjusted = new List<ModelInput>();
            foreach (ModelInput input in ratings)
            {
                adjusted.Add(new ModelInput {
                    MovieId = input.MovieId, 
                    Rating = input.Rating, 
                    Timestamp = input.Timestamp, 
                UserId = input.UserId + 1f + Utils.Constants.trainingDataLastUser
            });
            }

            return adjusted;
        }
    }
}
