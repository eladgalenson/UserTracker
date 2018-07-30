using FriendsTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsTracker.ViewModels
{
    public class InvitationViewModel
    {
        public UserProfile Trackee { get; set; }

        public string email { get; set; }

        public string Status { get; set; }
    }
}
