using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FriendsTracker.Data;
using FriendsTracker.Data.Entities;
using FriendsTracker.Services;
using FriendsTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationTest3.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FriendsTracker.Controllers
{
    public class InvitationController : Controller
    {
        private IInvitationRepository _invitationRepository;
        private IUserTrackingRepository _userTrackingRepository;
        
        private IMapper _mapper;
        private IMailServices _mailServices;
        private ILogger<InvitationController> _logger;
        private UserManager<ApplicationUser> _userManager { get; set; }

        public InvitationController(IUserTrackingRepository userTrackingRepository, 
            IInvitationRepository invitationRepository, 
            IMapper mapper, 
            IMailServices mailServices, 
            ILogger<InvitationController> logger, 
            UserManager<ApplicationUser> userManager)
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
            _mailServices = mailServices;
            _logger = logger;
            _userManager = userManager;
            _userTrackingRepository = userTrackingRepository;
        }
        // GET: /<controller>/
        //view all existing invitations
        [Authorize]
        public IActionResult Index()
        {
            return View(Load());
        }

        private IEnumerable<InvitationViewModel> Load()
        {
            return _mapper.Map<IEnumerable<InvitationViewModel>>(_invitationRepository.GetAll(User.Identity.Name));
        }

        [Authorize]
        public async Task<IActionResult> Create(string email)
        {
            var trackerProfile = _userTrackingRepository.GetUserProfile(User.Identity.Name);

            //todo - try to find user by email, if this is a new user create a profile and send email invite
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                //Create an invitation and send notification using application
                var trackeeProfile = _userTrackingRepository.GetUserProfile(user.UserName);
                var existing =  _invitationRepository.Get(trackerProfile.UserName, trackeeProfile.Id);

                if (existing != null)
                {
                    _logger.LogWarning("an inviate already exists");
                }
                else
                {
                    _invitationRepository.Add(new TrackingInvitation()
                    {
                        Status = eInvitationStatus.Invited,
                        TrackerId = trackerProfile.Id,
                        TrackeeId = trackeeProfile.Id,
                        Trackee = trackeeProfile
                    });
                    _mailServices.Send(User.Identity.Name, user.UserName, trackeeProfile.Id);
                }
                

            }
            else
            {
                //_userTrackingRepository.
                _userTrackingRepository.AddUserProfile(new UserProfile()
                {
                    AvatarType = Shared.Avatar.child.ToString(),
                    Created = DateTime.MinValue, // indicates its not created yet..
                    Status = "new user",
                    UserName = email
                });

                

                var trackeeProfile = _userTrackingRepository.GetUserProfile(email);

                //just create the profile and invitation and send an email
                _invitationRepository.Add(new TrackingInvitation()
                {
                    email = email,
                    Status = eInvitationStatus.Invited,
                    TrackerId = trackerProfile.Id,
                    TrackeeId = trackeeProfile.Id,
                    Trackee = trackeeProfile
                });
                _mailServices.Send(User.Identity.Name, email, trackeeProfile.Id);
            }

            // todo - move to an API call!
            return RedirectToAction("Index");
        }

        //[OverrideAuthentication]
        [Authorize]
        public IActionResult Send(int trackeeId, string email)
        {
            TrackingInvitation ti = null;
            if (trackeeId > 0)
            {
                ti = _invitationRepository.Get(User.Identity.Name, trackeeId);
                //send notifictaion via application messaging
            }
            else if (!string.IsNullOrEmpty(email))
            {
                ti = _invitationRepository.Get(User.Identity.Name, email);
                                
                try
                {
                    _mailServices.Send(User.Identity.Name, email, ti.TrackeeId);
                }
                catch(Exception exc)
                {
                    _logger.LogError(exc, "a problem when sending an invita email");
                }
                
                //send email

            }
            return RedirectToAction("Index");
            //return View(Load());
        }


        //API GET
        [HttpGet]

        public IActionResult Invitations(string userName)
        {
            return Ok(_invitationRepository.GetAll(userName));
        }
    }
}
