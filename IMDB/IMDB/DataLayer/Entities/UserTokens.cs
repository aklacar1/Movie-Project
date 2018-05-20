using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class UserTokens
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Users User { get; set; }
    }
}
