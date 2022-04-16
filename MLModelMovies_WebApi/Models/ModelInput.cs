using Microsoft.ML.Data;

namespace MLModelMovies_WebApi.Models
{
    /// <summary>
    /// model input class for MLModelMovies.
    /// </summary>
    #region model input class
    public class ModelInput
    {
        [ColumnName(@"userId")]
        public float UserId { get; set; }

        [ColumnName(@"movieId")]
        public float MovieId { get; set; }

        [ColumnName(@"rating")]
        public float Rating { get; set; }

        [ColumnName(@"timestamp")]
        public string Timestamp { get; set; }

    }

    #endregion
}
