using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Movie
    {

        public long MovieId { get; set; }
        public long? CompanyId { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Image { get; set; }
        public decimal Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string TrailerLink { get; set; }

        public Company Company { get; set; }
        public ICollection<MovieStaff> MovieStaff { get; set; }
        public ICollection<Genre> Genre { get; set; }
        internal ICollection<Rating> RatingNavigation { get; set; }
    }
}
