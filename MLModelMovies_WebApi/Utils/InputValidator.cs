using MLModelMovies_WebApi.Models;
using System.Collections.Generic;
using System;

namespace MLModelMovies_WebApi.Utils
{
    public class InputValidator
    {
        public static void ValidateUserRatings(List<ModelInput> ratedMovies)
        {
            if (ratedMovies.Count == 0) throw new ArgumentException("The user needs to have at least one rated movie");

            var userId = ratedMovies[0].UserId;
            if (userId < 0) throw new ArgumentException("Invalid userId");

            foreach (ModelInput rating in ratedMovies)
            {
                if (rating.UserId != userId) throw new ArgumentException("All the ratings need to have the same user");
                if (rating.MovieId < 0) throw new ArgumentException("Invalid MovieID");
                if (rating.Rating < 0 || rating.Rating > 5) throw new ArgumentException("Invalid Rating for movie");
            }
        }
    }
}
