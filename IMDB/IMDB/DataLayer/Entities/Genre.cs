using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Genre
    {
        public long GenreId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        internal ICollection<MovieGenres> MovieGenres { get; set; }
    }
}
