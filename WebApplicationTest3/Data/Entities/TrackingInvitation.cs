using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
    public class TrackingInvitation
    {
        public int TrackerId { get; set; }

        public int TrackeeId { get; set; }

        public string email { get; set; }
    }
}
