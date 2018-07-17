using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsTracker.Data.Entities;
using FriendsTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FriendsTracker.Controllers
{
    public class AccountController : Controller
    {
        private ILogger<AccountController> _logger;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _config;

        public AccountController(ILogger<AccountController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            this._logger = logger;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._config = config;
        }
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                RedirectToAction("All", "UserTracking");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    
                    if (result.Succeeded)
                    {
                        if (Request.Query.ContainsKey("ReturnUrl"))
                        {
                            Redirect(Request.Query["ReturnUrl"].First());
                        }
                        else
                        {
                            return RedirectToAction("All", "UserTracking");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogWarning("failure when trying to login", exc);
            }

            ModelState.AddModelError("", "Failed to Login");
            return View();
        }
    }
}
