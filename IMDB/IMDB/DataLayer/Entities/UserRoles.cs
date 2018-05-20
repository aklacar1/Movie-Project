
using System;
using System.Collections.Generic;

namespace IMDB.DataLayer.Entities
{
    public partial class UserRoles 
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        internal Roles Role { get; set; }
        internal Users User { get; set; }
    }
}
