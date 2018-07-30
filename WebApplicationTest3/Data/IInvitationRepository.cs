using FriendsTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Data
{
    public interface IInvitationRepository
    {
        IEnumerable<TrackingInvitation> GetAll(string userName);

        void Add(TrackingInvitation invitation);
        TrackingInvitation Get(string userName, int trackeeId);
        TrackingInvitation Get(string userName, string email);
    }
}
