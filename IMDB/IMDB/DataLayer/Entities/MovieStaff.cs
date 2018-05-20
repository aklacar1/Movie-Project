using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class MovieStaff
    {
        public long Id { get; set; }
        public long MovieId { get; set; }
        public long PersonJobsId { get; set; }

        internal Movie Movie { get; set; }
        public PersonJobs PersonJobs { get; set; }
    }
}
