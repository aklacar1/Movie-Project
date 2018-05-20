using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class PersonJobs
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public long PersonId { get; set; }

        public Job Job { get; set; }
        public Person Person { get; set; }
        internal ICollection<MovieStaff> MovieStaff { get; set; }
    }
}
