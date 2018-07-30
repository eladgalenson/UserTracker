using FriendsTracker.Data.Entities;
using System;
using System.Collections.Generic;

namespace WebApplicationTest3.Data
{
    public interface IUserTrackingRepository
    {
        IEnumerable<UserTracking> GetUserTrackings(string userId);

        IEnumerable<UserTracker> GetTrackers();

        void AddUserTracking(string userId, UserTracking info);

        void RemoveUserTracking(string userId, UserTracking info);

        //todo - move to a different repo
        UserProfile GetUserProfile(string userName);

        void AddUserProfile(UserProfile up);

        void UpdateUserProfile(UserProfile profile);

    }
}
