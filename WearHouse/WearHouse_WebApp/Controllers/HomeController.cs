using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Models;
using WearHouse_WebApp.Models.ViewModels;
using WearHouse_WebApp.Data;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Persistence;

namespace WearHouse_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly UnitOfWorkGettingWearables _unitOfWork;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            _unitOfWork = new UnitOfWorkGettingWearables(dbContext, userManager, "DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");
        }

        public IActionResult Index()
        {
            var wearables = dbContext.dbWearables
                .Where(l => l.State != "Inactive")
                .Select(item => item.ConvertToModel())
                .ToList();
            /*.Username == userManager.Users.Where(uid => item.UserId == uid.Id).First().UserName*/
            foreach(var post in wearables)
            {
                post.Owner = userManager.Users.Where(uid => post.dbModel.UserId == uid.Id).First();
            }
            return View("LandingPage", wearables);
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

            List<dbWearable> wearables = new List<dbWearable>();
            foreach (var dbmodel in dbContext.dbWearables
                .Where(m => m.UserId == id))
            {
                wearables.Add(dbmodel);
            }

            user.Wearables = wearables;

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

        public IActionResult WearablePost(int id)
        {
            WearableModel wearableModel = dbContext.dbWearables.FirstOrDefault(w => w.WearableId == id).ConvertToModel();
            return View(wearableModel);           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
