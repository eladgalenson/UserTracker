using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
    public class UserTracker
    {
        public int TrackerId { get; set; }
        public UserProfile Tracker { get; set; }
        public int TrackerCount { get; set; }
    }
}
