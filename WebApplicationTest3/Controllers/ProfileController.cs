using AutoMapper;
using FriendsTracker.Data.Entities;
using FriendsTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTest3.Data;

namespace FriendsTracker.Controllers
{

    public class ProfileController : Controller
    {

        IUserTrackingRepository _userTrackingRepository;
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IMapper _mapper;

        public ProfileController(IUserTrackingRepository userTrackingRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userTrackingRepository = userTrackingRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            // use automapper to change to UserProfileViewModel
            var result = await GetCurrentProfile();
            return View(result);
        }

        private async Task<UserProfileViewModel> GetCurrentProfile()
        {
            //get current profile
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);

            var profileModel = new UserProfileViewModel() { UserName = this.User.Identity.Name, AvatarType =  Shared.Avatar.child.ToString() };

            var profile = _userTrackingRepository.GetUserProfile(user.UserName);

            if (profile != null)
            {
                profileModel =  _mapper.Map<UserProfile, UserProfileViewModel>(profile);

                //profileModel.UserName = profile.ApplicationUserName;
                //profileModel.AvatarType = profile.AvatarType.ToString();
                //profileModel.Status = profile.Status;
                //profileModel.Gender = false;
                //profileModel.ImageUrl = profile.ImageUrl;
            }
            return profileModel;
        }

        public IActionResult Create()
        {
            //used to carete a new profile for a new user that accepted an invitation
            ViewData[""] = "In ProfileController";
            return View();
        }

        public IActionResult Update()
        {
            var result = GetCurrentProfile();
            return View(result.Result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(this.User.Identity.Name);

                    var currentProfile = _userTrackingRepository.GetUserProfile(user.UserName);

                    if (currentProfile == null)
                    {
                        currentProfile = _mapper.Map<UserProfileViewModel, UserProfile>(model);
                    }
                    else
                    {
                        currentProfile.Status = model.Status;
                        currentProfile.AvatarType = model.AvatarType;
                    }

                    if (model.ImageUpload != null && model.ImageUpload.FileName != null && model.ImageUpload.Length > 0)
                    {
                        //validate extension
                        var supportedTypes = new[] { "gif", "png", "jpeg" };
                        var fileExt = System.IO.Path.GetExtension(model.ImageUpload.FileName).Substring(1);
                        if (!supportedTypes.Contains(fileExt))
                        {
                            ViewData["Problem"] = "file upload type is not supported";
                            return View(model);
                        }

                        var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads",
                        model.ImageUpload.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageUpload.CopyToAsync(stream);
                        }

                        currentProfile.ImageUrl = "/uploads/" + model.ImageUpload.FileName;
                        model.ImageUrl = currentProfile.ImageUrl;
                    }

                   
                    _userTrackingRepository.UpdateUserProfile(currentProfile);
                    return RedirectToAction("Index");
                }
                catch(Exception exc)
                {
                    ViewData["Problem"] = $"exception ocured: {exc.Message}";
                }
                
            }
            else
            {
                //set some error message in form
                ViewData["Problem"] = "Invalid form  data";
            }

            //todo code to update profile
            
            //make sure its an image file and save
            return View(model);
        }
    }

}
