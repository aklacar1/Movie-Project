using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Rating
    {
        public long RatingId { get; set; }
        public long MovieId { get; set; }
        public string UserId { get; set; }
        public int MovieRating { get; set; }
        internal Movie Movie { get; set; }
        internal Users User { get; set; }
    }
}
