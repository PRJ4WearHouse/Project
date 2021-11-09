 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
 using WearHouse_WebApp.Models.dbModels;
using WearHouse_WebApp.Models.ViewModels;
 using WearHouse_WebApp.Persistence.Core.RepositoriesIF;
 using WearHouse_WebApp.Persistence.Repositories;

namespace WearHouse_WebApp.Controllers
{
    public class WearablesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageRepository _storage;

        //private readonly UserManager<ApplicationUser> _userManager;

        //To save files on public server
        BlobServiceClient blobServiceClient;

        public WearablesController(/*UserManager<ApplicationUser> userManager,*/ ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            //_userManager = userManager;
            _context = context;
            blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");
            _storage = new LocalImageRepository(hostEnvironment.WebRootPath);
        }
        /*
        // GET: Wearables
        public async Task<IActionResult> Index()
        {
            return View(await _context.dbWearables.ToListAsync());
        }*/
        

        // GET: Wearables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wearables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WearableId,Title,Description,Username,ImageFiles")] WearableViewModel wearableViewModel)
        {
            if (ModelState.IsValid)
            {
                //Save images from post thing
                //Also need to only save / delete again, if not succesfull on database
                wearableViewModel.ImageUrlsList = await _storage.SaveImages(wearableViewModel.WearableId, wearableViewModel.ImageFiles);
                if (wearableViewModel.ImageUrlsList != null)
                {
                    //For debugging
                    Console.WriteLine("No images saved");
                }
                //OBS delete this
                wearableViewModel.State = WearableState.Selling;
                wearableViewModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //add wearableViewModel to db. OBS Check username is correct and logged in!
                _context.Add(new dbWearable(wearableViewModel));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wearableViewModel);
        }

        async Task<bool> SaveImagesWearableImagesBlob(WearableViewModel wearableViewModel)
        {
            //OBS Could make sense to check if already exists first. https://stackoverflow.com/questions/52561285/how-to-upload-image-from-asp-net-core-iformfile-to-azure-blob-storage/52561985
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync("itemid"+wearableViewModel.WearableId.ToString());

            foreach (var image in wearableViewModel.ImageFiles)
                await containerClient.UploadBlobAsync(image.FileName, image.OpenReadStream());

            return true;
        }
        
        /*
        private bool WearableExists(int id)
        {
            return _context.dbWearables.Any(e => e.WearableId == id);
        }*/
    }
}
