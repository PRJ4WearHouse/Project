﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Models;
using WearHouse_WebApp.Models.ViewModels;

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

        public IActionResult WearablePost(WearableViewModel wearableViewModel)
        {
            // Preset wearableViewModel post
            WearableViewModel wearableViewModelMock = new(
                "Adidas shorts",
                "Nice, comfortable and sporty shorts for running and at-home comfort",
                "/Default/Image",
                "DefaultUsername",
                "DefaultContactInfo"
            );

#if DEBUG
            if (wearableViewModel.Title == null)
                ViewData["wearableViewModel"] = wearableViewModelMock;
            else
                ViewData["wearableViewModel"] = wearableViewModel;
#else
            if(wearableViewModel.Title == null)
                return View("Error");
            else
                ViewData["wearableViewModel"] = wearableViewModel;
#endif

            return View("WearablePost");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
