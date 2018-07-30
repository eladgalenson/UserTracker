using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsTracker.Data.Entities;
using Microsoft.Extensions.Logging;

namespace FriendsTracker.Data
{
    public class InvitationRepository : IInvitationRepository
    {
        
        private UserTrackingContext _dbContext;
        private ILogger<InvitationRepository> _logger;
        public InvitationRepository(UserTrackingContext context, ILogger<InvitationRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public void Add(TrackingInvitation invitation)
        {
            _logger.LogDebug($"TrackerId: {invitation.TrackerId}, TrackeeId: {invitation.TrackeeId}, email: {invitation.email}");
            _dbContext.TrackingInvitations.Add(invitation);
            _dbContext.SaveChanges();
        }

        public TrackingInvitation Get(string userName, int trackeeId)
        {
            var trackerUser = _dbContext.UserProfiles.Where(up => up.UserName == userName).FirstOrDefault();

            if (trackerUser == null)
            {
                _logger.LogError($"non exitent tracker {userName}");
            }

            var invitation = _dbContext.TrackingInvitations.Where(ti => ti.TrackerId == trackerUser.Id && ti.TrackeeId == trackeeId).FirstOrDefault();
            return invitation;
        }

        public TrackingInvitation Get(string userName, string email)
        {
            var trackerUser = _dbContext.UserProfiles.Where(up => up.UserName == userName).FirstOrDefault();

            if (trackerUser == null)
            {
                _logger.LogError($"non exitent tracker {userName}");
            }

            var invitation = _dbContext.TrackingInvitations.Where(ti => ti.TrackerId == trackerUser.Id && ti.email == email).FirstOrDefault();
            return invitation;

        }

        public IEnumerable<TrackingInvitation> GetAll(string userName)
        {
            _logger.LogDebug("Enter GetAll");

            var trackerUser = _dbContext.UserProfiles.Where(up => up.UserName == userName).FirstOrDefault();

            if (trackerUser == null)
            {
                _logger.LogError($"non exitent tracker {userName}");
            }
            var invitations = _dbContext.TrackingInvitations.Where(ti=>ti.TrackerId == trackerUser.Id).ToList();

            //invitations =  new List<TrackingInvitation>()
            //{
            //    new TrackingInvitation()
            //    {
            //        TrackerId = 1,
            //        TrackeeId = 5,
            //        email = "fourthuser",
            //        Status = eInvitationStatus.Invited
            //    },
            //    new TrackingInvitation()
            //    {
            //        TrackerId = 1,
            //        TrackeeId = 3,
            //        Status = eInvitationStatus.Invited
            //    }
            //};

            foreach (var i in invitations)
            {
                i.Status = eInvitationStatus.Invited;
                var profile = _dbContext.UserProfiles.Where(up => up.Id == i.TrackeeId).FirstOrDefault();
                if (profile != null && profile.Created > DateTime.MinValue)
                {
                    i.Trackee = profile; //todo - invitation migration needs to include profile as well
                }
            }
            return invitations;
        }
    }
}
