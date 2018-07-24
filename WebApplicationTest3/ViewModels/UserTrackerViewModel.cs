using FriendsTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.ViewModels
{
    public class UserTrackerViewModel
    {
        public UserProfileViewModel Tracker { get; set; }

        [DisplayName("User Tracking Count")]
        public int TrackerCount { get; set; }
    }
}
