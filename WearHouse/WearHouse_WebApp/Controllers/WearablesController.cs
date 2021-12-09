using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence;

namespace WearHouse_WebApp.Controllers
{
    public class WearablesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WearablesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,IWebHostEnvironment hostEnvironment, IConfiguration config, IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = (_unitOfWork == null)
                ? new UnitOfWork(context, userManager,config["ConnectionsStrings:StorageConnection"])
                : unitOfWork;
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
        public async Task<IActionResult> Create([Bind("Title,Description,ImageFiles,State")] WearableModel wearable)
        {
            if (ModelState.IsValid)
            {
                //Get user
                wearable.Owner = _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModelWithoutWearables();

                if(await _unitOfWork.SaveWearableWithImages(wearable))
                    return RedirectToAction("Profile", "Home", new {wearable.Owner.UserId });
            }
            return View();
        }

        //Create Comment
        public async Task<IActionResult> CreateComment([Bind("Comment,WearableId")] CommentModel commentToBeCreated)
        {
            commentToBeCreated.Moment = DateTime.Now;
            commentToBeCreated.Author = _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModel();
            await _unitOfWork.CommentRepository.Add(commentToBeCreated.ConvertToDbModel());
            await _unitOfWork.Complete();
            return View();
        }
    }
}