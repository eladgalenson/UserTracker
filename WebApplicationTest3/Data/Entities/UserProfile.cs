using FriendsTracker.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string UserName { get; set; } // will be the user name from identity

        public bool Gender { get; set; }

        public string ImageUrl { get; set; }

        public string AvatarType { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        [MaxLength(256), MinLength(5)]
        public string Status { get; set; }
    }
}
