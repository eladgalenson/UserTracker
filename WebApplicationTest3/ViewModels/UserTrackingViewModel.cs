using FriendsTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.ViewModels
{
    public class UserTrackingViewModel
    {
        public UserProfileViewModel UserProfile { get; set; }
        public UserOnlinePresenceViewModel OnlinePresence { get; set; }
    }
}
