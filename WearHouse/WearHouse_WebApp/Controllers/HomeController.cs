using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WearHouse_WebApp.Models.ViewModels;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;

namespace WearHouse_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = (unitOfWork == null)
                ? new UnitOfWork(dbContext, userManager,
                    "DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net")
                : unitOfWork;
        }

        public IActionResult Index()
        {
            List<WearableModel> wearables = _unitOfWork.Wearables
                .GetAllWearablesWithUsers().Result
                .Select(item => item.ConvertToWearableModel())
                .ToList();
            return View("LandingPage", wearables);
        }

        public IActionResult Users()
        {
            var users = _unitOfWork.UserRepository.GetAll().Result.ToList();
            return View(users);
        }

        [Route("/Home/Profile/{UserId}")]
        public IActionResult Profile(string UserId = null)
        {
            if (UserId == null)
                return View();
            ApplicationUser applicationUser = _unitOfWork.UserRepository.GetUserWithWearables(UserId).Result;
            UserModel user= applicationUser.ConvertToUserModel();

            return View(user);
        }

        public IActionResult Posts()
        {
            return View("Posts");
        }

        public IActionResult WearablePost(int id)
        {

            WearableModel wearableModel = new WearableModel(_unitOfWork.Wearables.GetWearableWithComments(id).Result, true);
            WearableViewModel model = new WearableViewModel { Wearable = wearableModel };
            return View(model);           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment([Bind("CommentToAdd,ID","Wearable")] Models.ViewModels.WearableViewModel model)
        {
            CommentModel newComment = new CommentModel();
            newComment.Moment = DateTime.Now;
            newComment.Comment = model.CommentToAdd;
            newComment.Author = _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModelWithoutWearables();
            newComment.WearableId = model.Wearable.ID;

            model.Wearable = new WearableModel(_unitOfWork.Wearables.GetWearableWithComments(model.Wearable.ID).Result, true);
            model.Wearable.Comments.Add(newComment);
            await _unitOfWork.CommentRepository.Add(newComment.ConvertToDbModel());
            await _unitOfWork.Complete();


            model.CommentToAdd = null;

            return View("WearablePost", model);
        }
    }
}
