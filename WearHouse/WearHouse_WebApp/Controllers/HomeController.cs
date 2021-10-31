﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Models;

namespace WearHouse_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View("LandingPage");
        }

        public IActionResult Users()
        {
            var users = userManager.Users;
            return View(users);
        }

        public IActionResult Profile(string id = null)
        {
            if (id == null)
                return View();

            ApplicationUser user = userManager.Users.First(u => u.Id == id);
            return View(user);
        }

        public IActionResult Posts()
        {
            return View("Posts");
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        public IActionResult WearablePost(WearablePost wearablePost = null)
        {
            // Preset wearable post
            Wearable wearable = new(25, "" , "Adidas", "Mens shorts, light fabric, good for running", Gender.Mens);
            WearablePost wearablePostMock = new(
                wearable,
                WearablePostState.Sale,
                9.95
            );

            /* Udkommenteres i release
            
            ViewData["wearable"] = wearablePost;

             */

            ViewData["wearable"] = wearablePostMock;

            return View("WearablePost");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
