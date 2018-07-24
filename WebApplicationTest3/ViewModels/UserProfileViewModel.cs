using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [MinLength(5)]
        [MaxLength(256)]
        public string Status { get; set; }
        
        public bool? Gender { get; set; }

        [Url]
        [Display(Name = "Profile Image")]
        public string ImageUrl { get; set; }

       
        public IFormFile ImageUpload { get; set; }

        public string AvatarType { get; set; }
    }
}
