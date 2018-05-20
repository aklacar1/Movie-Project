using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Person
    {

        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        internal ICollection<PersonJobs> PersonJobs { get; set; }
        internal ICollection<Users> Users { get; set; }
    }
}
