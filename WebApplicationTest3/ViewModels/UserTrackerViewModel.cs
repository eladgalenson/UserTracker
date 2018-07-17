using FriendsTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.ViewModels
{
    public class UserTrackerViewModel
    {
        public UserProfile UserProfile { get; set; }
        public int TrackingCount { get; set; }
    }
}
