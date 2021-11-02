﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models;
using Azure.Storage.Blobs;

namespace WearHouse_WebApp.Controllers
{
    public class WearablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        //To save files locally on server
        private readonly IWebHostEnvironment hostEnvironment;

        //To save files on public server
        BlobServiceClient blobServiceClient;

        public WearablesController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
            blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");

        }

        // GET: Wearables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wearables.ToListAsync());
        }

        // GET: Wearables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wearable = await _context.Wearables
                .FirstOrDefaultAsync(m => m.WearableId == id);
            if (wearable == null)
            {
                return NotFound();
            }

            return View(wearable);
        }

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
        public async Task<IActionResult> Create([Bind("WearableId,Title,Description,ImageUrls,Username,ImageFile")] Wearable wearable)
        {
            if (ModelState.IsValid)
            {
                //await SaveImagesWearableImagesLocally(wearable);
                await SaveImagesWearableImagesBlob(wearable);

                //add wearable to db. OBS Check username is correct and logged in!
                _context.Add(wearable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wearable);
        }

        async Task<bool> SaveImagesWearableImagesBlob(Wearable wearable)
        {
            //OBS Could make sense to check if already exists first. https://stackoverflow.com/questions/52561285/how-to-upload-image-from-asp-net-core-iformfile-to-azure-blob-storage/52561985
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync("itemid"+wearable.WearableId.ToString());

            foreach (var image in wearable.ImageFile)
                await containerClient.UploadBlobAsync(image.FileName, image.OpenReadStream());

            return true;
        }

        async Task<bool> SaveImagesWearableImagesLocally(Wearable wearable)
        {
            if (wearable.ImageFile != null)
            {
                //create image in Files and save it.
                string wwwRootPath = hostEnvironment.WebRootPath;
                foreach (var image in wearable.ImageFile)
                {
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    wearable.ImageUrls = fileName = wearable.Title + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
                return true;
            }
            else
                return false;
        }

        // GET: Wearables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wearable = await _context.Wearables.FindAsync(id);
            if (wearable == null)
            {
                return NotFound();
            }
            return View(wearable);
        }

        // POST: Wearables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WearableId,Title,Description,ImageFile,ImageUrls")] Wearable wearable)
        {
            if (id != wearable.WearableId)
            {
                return NotFound();
            }
            if(wearable.ImageFile!=null)
            {
                /*
                //delete old image
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, "Image", wearable.ImageUrls);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
                //create new filename
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(wearable.ImageFile.FileName);
                string extension = Path.GetExtension(wearable.ImageFile.FileName);
                wearable.ImageUrls = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await wearable.ImageFile.CopyToAsync(fileStream);

                }*/
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wearable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WearableExists(wearable.WearableId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wearable);
        }

        // GET: Wearables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wearable = await _context.Wearables
                .FirstOrDefaultAsync(m => m.WearableId == id);
            if (wearable == null)
            {
                return NotFound();
            }

            return View(wearable);
        }

        // POST: Wearables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wearable = await _context.Wearables.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(hostEnvironment.WebRootPath, "Image", wearable.ImageUrls);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            //delete record of wearable
            _context.Wearables.Remove(wearable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WearableExists(int id)
        {
            return _context.Wearables.Any(e => e.WearableId == id);
        }
    }
}
