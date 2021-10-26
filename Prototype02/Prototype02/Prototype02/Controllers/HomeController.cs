using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prototype02.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("LandingPage");
        }

        public IActionResult Profile()
        {
            return View("Profile");
        }

        public IActionResult Posts()
        {
            return View("Posts");
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
