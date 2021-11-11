using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.ViewModels;
using WearHouse_WebApp.Persistence;
using WearHouse_WebApp.Persistence.Core;
using WearHouse_WebApp.Persistence.Repositories;
using WearableState = WearHouse_WebApp.Models.ViewModels.WearableState;

namespace WearHouse_WebApp.Controllers
{
    public class WearablesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public WearablesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            _unitOfWork = new UnitOfWorkCreatingWearable(context, "DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");
        }

        // GET: Wearables/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wearables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ImageFiles","dbModel")] WearableModel wearable)
        {
            if (ModelState.IsValid)
            {
                //Get user
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                wearable.dbModel.UserId = currentUser.Id;

                //Add model to db
                await _unitOfWork.Wearables.Add(wearable.dbModel);

                //Save changes to generate unchangeable wearableID. 
                await _unitOfWork.Complete();

                //Get wearableID
                int itemId = wearable.dbModel.WearableId;

                //Save images at 
                if (wearable.ImageFiles != null)
                {
                    wearable.dbModel.ImageUrls = await _unitOfWork.ImageStorage.SaveImagesToWearable(wearable.ImageFiles, itemId);

                    //Save changes
                    await _unitOfWork.Complete();
                }
                
                //Redirect
                return RedirectToAction("Profile", "Home", new {currentUser.Id });
            }
            return View(wearable);
        }
    }
}