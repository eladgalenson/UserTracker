using System;
using System.Collections.Generic;
using System.Linq;
using FriendsTracker.Data;
using FriendsTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationTest3.Data
{
    public class UserTrackingRepository : IUserTrackingRepository
    {
        private UserTrackingContext _dbContext;
        public UserTrackingRepository(UserTrackingContext context)
        {
            _dbContext = context;
        }

        public void AddUserTracking(string userId, UserTracking info)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<FriendModel> GetFriends(int friendId)
        //{
        //    return new List<FriendModel>()
        //    {
        //        new FriendModel() { FirstName = "Galia", LastName="Galenson", Id=2, IsOnline=false, Location = "Hazavim 1, Petah-Tikva"},
        //        new FriendModel() { FirstName = "Zohar", LastName="Galenson", Id=3, IsOnline=false, Location = "Hazavim 1, Petah-Tikva"},
        //    };
        //}
      

        //public IEnumerable<UserProfile> GetTrackers()
        //{
        //    var trackes = _dbContext.UserTrackings.Include(ut => ut.Tracker).
        //        Select(ut => ut.Tracker).ToList();
        //    return trackes;


        //}

        public UserProfile GetUserProfile(int Id)
        {
            return _dbContext.UserProfiles.Where(p => p.Id == Id).FirstOrDefault();
        }

        private UserProfile GetUserProfile(string userName)
        {
            return _dbContext.UserProfiles.Where(p => p.ApplicationUserName == userName).FirstOrDefault();
        }

      

        public IEnumerable<UserTracking> GetUserTrackings(string userName)
        {
            var profile = GetUserProfile(userName);
            if (profile !=null)
            {
                var uts =  _dbContext.UserTrackings.Include(ut => ut.User).Include(u=>u.UserOnlinePresence).Where(t => t.TrackerId == profile.Id).ToList();
                return uts;
            }
            return null;
        }

        public IEnumerable<UserTracker> GetTrackers()
        {
            List<UserTracker> userTrackings = new List<UserTracker>();
            var utGrouping = _dbContext.UserTrackings.Include(ut => ut.Tracker).GroupBy(c => c.Tracker);
            foreach(var group in utGrouping)
            {
                userTrackings.Add(new UserTracker()
                {
                    Tracker = group.Key,
                    TrackerCount = group.Count()
                });
            }
            return userTrackings;



        }

        public void RemoveUserTracking(string userId, UserTracking info)
        {
            throw new NotImplementedException();
        }

 
    }
}
