using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data.Entities
{
    public enum eInvitationStatus
    {
        Invited,
        Accepted,
        Rejected
    }
    public class TrackingInvitation
    {
        public int TrackerId { get; set; }

        public int TrackeeId { get; set; }

        public UserProfile Trackee { get; set; }

        public string email { get; set; }

        [NotMapped]
        public eInvitationStatus Status { get; set; }
    }
}
