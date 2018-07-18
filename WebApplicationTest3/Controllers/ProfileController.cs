using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsTracker.Controllers
{
    
    public class ProfileController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData[""] = "In ProfileController";
            return View();
        }
    }

}
