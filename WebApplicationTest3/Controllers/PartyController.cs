﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest3.Controllers
{
    public class PartyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}