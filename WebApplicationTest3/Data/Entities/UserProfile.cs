using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string ApplicationUserName { get; set; } // will be the user name from identity

        public bool Gender { get; set; }

        public string ImageUrl { get; set; }

        public string AvatarType { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
