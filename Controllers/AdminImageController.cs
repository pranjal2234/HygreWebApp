using HygreWebApp.Data;
using HygreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HygreWebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminImageController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // LIST IMAGES
        public IActionResult Index()
        {
            var images = _context.SiteImages.ToList();
            return View(images);
        }

        // UPLOAD IMAGE
        [HttpPost]
        public IActionResult Upload(IFormFile imageFile, string category)
        {
            if (imageFile == null || imageFile.Length == 0)
                return RedirectToAction("Index");

            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "home");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            var image = new SiteImage
            {
                FileName = uniqueFileName,
                FilePath = "/uploads/home/" + uniqueFileName,
                Category = category
            };

            _context.SiteImages.Add(image);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE IMAGE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var image = _context.SiteImages.FirstOrDefault(i => i.Id == id);
            if (image == null)
                return RedirectToAction("Index");

            string fullPath = Path.Combine(_env.WebRootPath, image.FilePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _context.SiteImages.Remove(image);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // REPLACE IMAGE
        [HttpPost]
        public IActionResult Replace(int id, IFormFile newImage)
        {
            var image = _context.SiteImages.FirstOrDefault(i => i.Id == id);
            if (image == null || newImage == null)
                return RedirectToAction("Index");

            string oldPath = Path.Combine(_env.WebRootPath, image.FilePath.TrimStart('/'));
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "home");
            string newFileName = Guid.NewGuid() + Path.GetExtension(newImage.FileName);
            string newFilePath = Path.Combine(uploadsFolder, newFileName);

            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                newImage.CopyTo(stream);
            }

            image.FileName = newFileName;
            image.FilePath = "/uploads/home/" + newFileName;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
