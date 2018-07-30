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

        private RoleManager<ApplicationRole> _roleManager { get; set; }

        public SeedGenerator(UserTrackingContext context, IHostingEnvironment hosting, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._context = context;
            this._hosting = hosting;
            this._userManager = userManager;
            this._roleManager = roleManager;
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
                    Email = "firstuser@gmail.com",
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

            user = await this._userManager.FindByEmailAsync("adminuser@gmail.com");
            if (user == null)
            {
                var adminUser = new ApplicationUser()
                {
                    FirstName = "admin",
                    LastName = "user",
                    UserName = "adminuser",
                    Email = "adminuser@gmail.com",
                };
                var result3 = await this._userManager.CreateAsync(adminUser, "123456aB!");
                if (result3 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default users");
                }

                var role = new ApplicationRole() { Name = "Admin", Description = "Administrator role" };
                var result4 = await this._roleManager.CreateAsync(role);
                if (result4 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create admin roles");
                }

                var result5 = await this._userManager.AddToRoleAsync(adminUser, "Admin");
                if (result5 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create admin roles");
                }
            }


            if (!_context.UserTrackings.Any())
            {
                var firstUser = await this._userManager.FindByEmailAsync("firstuser@gmail.com");

                var profileFirst = new UserProfile()
                {
                    UserName = firstUser.UserName,
                    AvatarType = Shared.Avatar.meh.ToString(),
                    Created = DateTime.Now,
                    Gender = true,
                    ImageUrl = string.Empty
                };

                _context.UserProfiles.Add(profileFirst);
                //_context.UserProfiles.Add(profileFirst);
                var secondtUser = await this._userManager.FindByEmailAsync("seconduser@gmail.com");

                var profileSecond = new UserProfile()
                {
                    UserName = secondtUser.UserName,
                    AvatarType = Shared.Avatar.poo.ToString(),
                    Created = DateTime.Now,
                    Gender = true,
                    ImageUrl = string.Empty
                };

                _context.UserProfiles.Add(profileSecond);
                _context.SaveChanges();

                var trackedProfile = _context.UserProfiles.Where(up => up.UserName == secondtUser.UserName).FirstOrDefault();
                var trackerProfile = _context.UserProfiles.Where(up => up.UserName == firstUser.UserName).FirstOrDefault();

                UserTracking ut = new UserTracking()
                {
                    OnlinePresence = new UserOnlinePresence()
                    {
                        IsActive = false,
                        Location = "somewhere in te mediratenean",
                        ProfileId = trackedProfile.Id
                    },
                    UserProfile = trackedProfile,
                    UserProfileId = trackedProfile.Id,
                    //                    Tracker = profileFirst,
                    TrackerId = trackerProfile.Id
                };


                _context.UserTrackings.Add(ut);
                _context.SaveChanges();
            }

            user = await this._userManager.FindByEmailAsync("thirduser@gmail.com");
            if (user == null)
            {


                user = new ApplicationUser()
                {
                    FirstName = "third",
                    LastName = "user",
                    UserName = "thirduser",
                    Email = "thirduser@gmail.com",
                };

                var result1 = await this._userManager.CreateAsync(user, "123456aB!");

                if (result1 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create third user");
                }


                var profileThird = new UserProfile()
                {
                    UserName = user.UserName,
                    AvatarType = Shared.Avatar.female.ToString(),
                    Created = DateTime.Now,
                    Gender = true,
                    ImageUrl = string.Empty
                };

                _context.UserProfiles.Add(profileThird);
                _context.SaveChanges();
            }

            user = await this._userManager.FindByEmailAsync("fourthuser@gmail.com");
            if (user == null)
            {
                // this will a user created fro invitation without actual application user
                var profileFourth = new UserProfile()
                {
                    UserName = "fourthuser@gmail.com",
                    Created = DateTime.MinValue
                };

                _context.UserProfiles.Add(profileFourth);
                _context.SaveChanges();
            }


            if (!_context.TrackingInvitations.Any())
            {
                var trackingProfile = _context.UserProfiles.Where(up => up.UserName == "firstuser").FirstOrDefault();
                var trackedProfile = _context.UserProfiles.Where(up => up.UserName == "thirduser").FirstOrDefault();
                var trackedProfileEmail = _context.UserProfiles.Where(up => up.UserName == "fourthuser@gmail.com").FirstOrDefault();

                if (trackingProfile == null || trackedProfile == null || trackedProfileEmail == null)
                {
                    throw new InvalidOperationException("Expected profiles are missing");
                }


                var invitation = new TrackingInvitation()
                {
                    TrackerId = trackingProfile.Id,
                    TrackeeId = trackedProfile.Id
                };

                var invitationViaEmail = new TrackingInvitation()
                {
                    TrackerId = trackingProfile.Id,
                    TrackeeId = trackedProfileEmail.Id,
                    email = trackedProfileEmail.UserName
                };

                _context.TrackingInvitations.Add(invitation);
                _context.TrackingInvitations.Add(invitationViaEmail);
                _context.SaveChanges();

            }
        }

    }
}
