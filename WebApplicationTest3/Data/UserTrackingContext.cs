using FriendsTracker.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data
{
    public class UserTrackingContext : IdentityDbContext<ApplicationUser>
    {
        public UserTrackingContext(DbContextOptions<UserTrackingContext> context) : base(context)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserTracking>().Property(k => k.TrackerId).IsRequired();
            builder.Entity<UserTracking>().Property(k => k.UserId).IsRequired();
            builder.Entity<UserTracking>().HasKey(k => new { k.UserId, k.TrackerId});
            builder.Entity<TrackingInvitation>().HasKey(k => new { k.TrackerId, k.TrackeeId});



            base.OnModelCreating(builder);
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<TrackingInvitation> TrackingInvitations { get; set; }

        public DbSet<UserTracking> UserTrackings { get; set; }

        


    }
}
