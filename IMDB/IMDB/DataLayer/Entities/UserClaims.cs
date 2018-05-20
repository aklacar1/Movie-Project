using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class UserClaims
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }

        public Users User { get; set; }
    }
}
