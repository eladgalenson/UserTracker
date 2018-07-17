using FriendsTracker.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data
{
    public class SeedGenerator
    {
        private UserTrackingContext _context { get; set; }
        private IHostingEnvironment _hosting { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }

        public SeedGenerator(UserTrackingContext context, IHostingEnvironment hosting, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._hosting = hosting;
            this._userManager = userManager;
        }

        public async Task Seed()
        {
            _context.Database.EnsureCreated();

            var user = await this._userManager.FindByEmailAsync("firstuser@gmail.com");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    FirstName = "first",
                    LastName = "user",
                    UserName = "firstuser",
                    Email = "firstuser@gmail.com"
                };

                var trackedUser = new ApplicationUser()
                {
                    FirstName = "second",
                    LastName = "user",
                    UserName = "seconduser",
                    Email = "seconduser@gmail.com"
                };

                var result1 = await this._userManager.CreateAsync(user, "123456aB!");
                var result2 = await this._userManager.CreateAsync(trackedUser, "123456aB!");
                if (result1 != IdentityResult.Success || result2 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default users");
                }
            }

            if (!_context.UserTrackings.Any())
            {
                var firstUser = await this._userManager.FindByEmailAsync("firstuser@gmail.com");

                var profileFirst = new UserProfile()
                {
                    UserId = firstUser.UserName,
                    AvatarType = "tough",
                    Created = DateTime.Now,
                    Gender = true,
                    ImageUrl = string.Empty
                };

                _context.UserProfiles.Add(profileFirst);
                //_context.UserProfiles.Add(profileFirst);
                var secondtUser = await this._userManager.FindByEmailAsync("seconduser@gmail.com");

                var profileSecond = new UserProfile()
                {
                    UserId = secondtUser.UserName,
                    AvatarType = "cute",
                    Created = DateTime.Now,
                    Gender = true,
                    ImageUrl = string.Empty
                };

                _context.UserProfiles.Add(profileSecond);

                UserTracking ut = new UserTracking()
                {
                    UserOnlinePresence = new UserOnlinePresence()
                    {
                        IsActive = false,
                        Location = "somewhere in te mediratenean",
                        UserId = profileSecond.Id,
                        User = profileSecond
                    },
                    User = profileSecond,
                    UserId = profileSecond.Id,
                    Tracker = profileFirst,
                    TrackerId = profileFirst.Id
                };
               

                _context.UserTrackings.Add(ut);
                _context.SaveChanges();
            }

        }

    }
}
