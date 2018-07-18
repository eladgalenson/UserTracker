using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
    public class UserOnlinePresence
    {
        public int Id { get; set; }
        public int ApplicationUserName { get; set; }

        //public UserProfile User { get; set; }

        public bool IsActive { get; set; }

        public string Location { get; set; }
    }
}
