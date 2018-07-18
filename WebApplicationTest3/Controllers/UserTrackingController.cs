using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsTracker.Data.Entities;
using FriendsTracker.ViewModels;
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

        public UserTrackingController(IUserTrackingRepository userTrackingRepository, UserManager<ApplicationUser> userManager)
        {
            _userTrackingRepository = userTrackingRepository;
            _userManager = userManager;
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

            {
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
                    return View(trackedUsers);
                }

                return View(new List<UserTracking>());
            }
        }

        
        public async Task<IActionResult> AllTrackers()
        {
            //simple comment
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = await _userManager.IsInRoleAsync(user, "Admin");
            if (result)
            {
                ViewData["access"] = "admin";
            }

            var trackedUsers = _userTrackingRepository.GetTrackers();
            return View(trackedUsers);
            
        }

        //API GET
        [HttpGet]
        
        public IActionResult GetAll()
        {
            var model = _userTrackingRepository.GetUserTrackings("96476fba-8626-4fc4-a33b-cf8210889855");
            return Ok(model);
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