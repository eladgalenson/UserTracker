using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
   

    public class UserTracking
    {
        public int TrackerId { get; set; }

        //public UserProfile Tracker { get; set; }

        public int UserProfileId { get; set; }


        
        public UserProfile UserProfile { get; set; }

        
        public UserOnlinePresence OnlinePresence { get; set; }
       
    }
}
