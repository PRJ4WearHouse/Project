using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence;

namespace WearHouse_WebApp.Controllers
{
    public class WearablesController : Controller
    {
        private readonly UnitOfWorkCreatingWearable _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public WearablesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            _unitOfWork = new UnitOfWorkCreatingWearable(context, userManager, "DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");
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
                wearable.Owner = await _userManager.GetUserAsync(HttpContext.User);

                if(await _unitOfWork.SaveWearableWithImages(wearable))

                //Redirect
                return RedirectToAction("Profile", "Home", new {wearable.Owner.Id });
            }
            return View(wearable);
        }
    }
}