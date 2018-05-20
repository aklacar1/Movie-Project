using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Genre
    {
        public long GenreId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long MovieId { get; set; }

        internal Movie Movie { get; set; }
    }
}
