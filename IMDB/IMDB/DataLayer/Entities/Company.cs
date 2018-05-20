using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Company
    {
        public long CompanyId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }

        internal ICollection<Movie> Movie { get; set; }
    }
}
