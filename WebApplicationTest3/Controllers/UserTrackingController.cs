using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FriendsTracker.Data.Entities;
using FriendsTracker.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest3.Data;

namespace WebApplicationTest3.Controllers
{
    public class UserTrackingController : Controller
    {
        IUserTrackingRepository _userTrackingRepository;
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IMapper _mapper;

        public UserTrackingController(IUserTrackingRepository userTrackingRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userTrackingRepository = userTrackingRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            //show infromation about friends
            return View();
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            if (!this.User.Identity.IsAuthenticated)
                throw new NotImplementedException();
            //show list of the current user user trackings
            {
                var vmTrackings = new List<UserTrackingViewModel>();

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var result = await _userManager.IsInRoleAsync(user, "Admin");
                if (result)
                {

                    return RedirectToAction("AllTrackers");
                }
                
                //get tracked users
                var trackedUsers = _userTrackingRepository.GetUserTrackings(user.UserName);
                
                if (trackedUsers != null && trackedUsers.Count() > 0)
                {
                    foreach(var tu in trackedUsers)
                    {
                        var vmTrackedUser = _mapper.Map<UserTrackingViewModel>(tu);
                        vmTrackings.Add(vmTrackedUser);
                    }
                    
                    return View(vmTrackings);
                }

                return View(vmTrackings);
            }
        }

        [Authorize]
        
        public async Task<IActionResult> AllTrackers()
        {
            //simple comment
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = await _userManager.IsInRoleAsync(user, "Admin");
            if (result)
            {
                ViewData["access"] = "admin";
            }

            var vmTrackers = new List<UserTrackerViewModel>();

            var trackers = _userTrackingRepository.GetTrackers();
            if (trackers != null && trackers.Count() > 0)
            {
                foreach (var t in trackers)
                {
                    var vmTracker = _mapper.Map<UserTrackerViewModel>(t);
                    vmTrackers.Add(vmTracker);
                }
            }
            return View(vmTrackers);
            
        }

        //API GET
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        
        public IActionResult Trackers()
        {
            return Ok(_userTrackingRepository.GetTrackers());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult Trackings(string userName)
        {
            return Ok(_userTrackingRepository.GetUserTrackings(userName));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(InvitationViewModel model)
        {
            //review
            return RedirectToAction("AllTrackers");
        }
    }
}