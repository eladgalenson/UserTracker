using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
    public class UserTracker
    {
        public UserProfile Tracker { get; set; }
        public int TrackerCount { get; set; }
    }

    public class UserTracking
    {
        public int TrackerId { get; set; }

        public UserProfile Tracker { get; set; }

        public int UserId { get; set; }

        public UserProfile User { get; set; }

        public UserOnlinePresence UserOnlinePresence { get; set; }
       
    }
}
