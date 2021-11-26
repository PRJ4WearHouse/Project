using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence;

namespace WearHouse_WebApp.Controllers
{
    public class WearablesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public WearablesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = new UnitOfWork(context, userManager, "DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");
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
                wearable.Owner = _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModelWithoutWearables();

                if(await _unitOfWork.SaveWearableWithImages(wearable))
                    return RedirectToAction("Profile", "Home", new {wearable.Owner.UserId });
            }
            return View();
        }

        //Create Comment
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateComment([Bind("CommentToAdd,Wearable")] Models.ViewModels.WearableViewModel model)
        //{
        //    //dbComments newComment = new CommentModel(Comment, DateTime.Now, _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModel()).ConvertToDbModel(wearableId);
        //    CommentModel newComment = new CommentModel();
        //    newComment.Moment = DateTime.Now;
        //    newComment.Comment = model.CommentToAdd;
        //    newComment.Author = _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModel();
        //    newComment.WearableId = model.Wearable.ID;
        //    model.Wearable.Comments.Add(newComment);
        //    await _unitOfWork.CommentRepository.Add(newComment.ConvertToDbModel());
        //    await _unitOfWork.Complete();
        //    model.CommentToAdd = null;
        //    return View(model);
        //}

        //Load
        //public async Task<IActionResult> GetAllCommentsForWearable(int wearableId)
        //{
        //    await _unitOfWork.Comment.
        //    await wearableId
        //}
    }
}