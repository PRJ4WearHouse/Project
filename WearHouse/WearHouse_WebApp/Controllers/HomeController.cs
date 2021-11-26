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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly UnitOfWork _unitOfWork;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            _unitOfWork = new UnitOfWork(dbContext, userManager, "DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");
        }

        public IActionResult Index()
        {
            List<WearableModel> wearables = _unitOfWork.Wearables
                .GetAllWearablesWithUsers().Result
                .Select(item => item.ConvertToWearableModel())
                .ToList();
            //var DbWearables = dbContext.dbWearables.ToList();
            //List<WearableModel> wearables = new();
            //foreach (var item in DbWearables)
            //{
            //    wearables.Add(new WearableModel(item, true));
            //}
            
            /*
            var wearables = dbContext.dbWearables
                .Where(l => l.State != "Inactive")
                .Select(item => item.ConvertToWearableModelWithoutOwner())
                .ToList();
            /*.Username == userManager.Users.Where(uid => item.UserId == uid.Id).First().UserName*/
            //foreach (var post in wearables)
            //{
            //    post.Owner = userManager.Users.Where(uid => post.dbModel.UserId == uid.Id).First().ConvertToUserModel();
            //}
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

            //ApplicationUser user = userManager.Users.First(u => u.Id == id);

            //List<dbWearable> wearables = new List<dbWearable>();
            //foreach (var dbmodel in dbContext.dbWearables
            //    .Where(m => m.UserId == id))
            //{
            //    wearables.Add(dbmodel);
            //}

            ApplicationUser applicationUser = _unitOfWork.UserRepository.GetUserWithWearables(id).Result;
            UserModel user= applicationUser.ConvertToUserModel();

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
            WearableModel wearableModel = new WearableModel(_unitOfWork.Wearables.GetSingleWearableWithUser(id).Result, true);
            WearableViewModel model = new WearableViewModel { Wearable = wearableModel };
            return View(model);           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment([Bind("CommentToAdd,Wearable")] Models.ViewModels.WearableViewModel model)
        {
            //dbComments newComment = new CommentModel(Comment, DateTime.Now, _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModel()).ConvertToDbModel(wearableId);
            CommentModel newComment = new CommentModel();
            newComment.Moment = DateTime.Now;
            newComment.Comment = model.CommentToAdd;
            newComment.Author = _unitOfWork.GetCurrentUserWithoutWearables(HttpContext).Result.ConvertToUserModelWithoutWearables();
            newComment.WearableId = model.Wearable.ID;
            model.Wearable.Comments.Add(newComment);
            await _unitOfWork.CommentRepository.Add(newComment.ConvertToDbModel());
            await _unitOfWork.Complete();
            model.CommentToAdd = null;
            return View(model);
        }
    }
}
