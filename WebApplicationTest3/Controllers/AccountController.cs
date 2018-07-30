﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FriendsTracker.Data.Entities;
using FriendsTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await this._userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        // do not use PasswordSignInAsync here since it will sign in with cookie
                        //calling CheckPasswordSignInAsync does not change state
                        var result = await this._signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                        if (result.Succeeded)
                        {
                            //logic to create token now:
                            var claims = new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unique value to represent the token
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var token = new JwtSecurityToken(

                                    _config["Tokens:Issuer"],
                                    _config["Tokens:Audience"],
                                    claims,
                                    expires: DateTime.UtcNow.AddMinutes(30),
                                    signingCredentials: creds
                                );

                            var results = new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            };

                            return Created("", results);
                        }
                    }
                }
                catch (Exception exc)
                {
                    _logger.LogError($"Problem in CreateToken {exc}");
                }

            }

            return BadRequest();
        }
    }


}
