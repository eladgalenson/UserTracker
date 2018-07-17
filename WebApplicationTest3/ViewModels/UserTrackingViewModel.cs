using FriendsTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.ViewModels
{
    public class UserTrackingViewModel
    {
        public int TrackingId { get; set; }

        public string UserId { get; set; }

        public bool Gender { get; set; }

        public string ImageUrl { get; set; }

        public string AvatarType { get; set; }

        public bool IsActive { get; set; }

        public string Location { get; set; }
    }
}
