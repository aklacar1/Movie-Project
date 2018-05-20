using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Job
    {

        public long JobId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        internal ICollection<PersonJobs> PersonJobs { get; set; }
    }
}
