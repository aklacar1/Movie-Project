using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class Users : IdentityUser
    {
        public long? PersonId { get; set; }
        internal Person Person { get; set; }
        internal ICollection<Rating> Rating { get; set; }
    }
}
