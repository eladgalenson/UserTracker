using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.ViewModels
{
    public class InvitationViewModel
    {
        [Required]
        public string Email { get; set; }        
        public string Avatar { get; set; }

        public string Message { get; set; }
    }
}
