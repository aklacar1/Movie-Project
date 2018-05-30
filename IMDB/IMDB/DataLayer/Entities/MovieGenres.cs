using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.DataLayer.Entities
{
    public partial class MovieGenres
    {
        public long Id { get; set; }
        public long MovieId { get; set; }
        public long GenreId { get; set; }

        internal Genre Genre { get; set; }
        internal Movie Movie { get; set; }
    }
}
