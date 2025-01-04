using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Controllers
{
    public class FileController : Controller
    {
        // GET: FileController
        public async Task<IActionResult> D(string name)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", name);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var contentType = "application/octet-stream";

            string ext = Path.GetExtension(name).ToLowerInvariant();

            switch (ext)
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".txt":
                    contentType = "text/plain";
                    break;
                    // Add more cases as needed
            }

            return File(fileBytes, contentType, name);
        }

        public async Task<IActionResult> SD(string name)
        {
            if (name.Contains("../"))
            {
                return BadRequest();
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files","uploads", name);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var contentType = "application/octet-stream";

            string ext = Path.GetExtension(name).ToLowerInvariant();

            switch (ext)
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".txt":
                    contentType = "text/plain";
                    break;
                    // Add more cases as needed
            }

            return File(fileBytes, contentType, name);
        }


        public IActionResult U()
        {

            return View();
        }

        [HttpPost]
        public async Task< IActionResult> U(IFormFile file)
        {
            if (file==null||file.Length==0)
            {
                ViewBag.message = "please select file";
                return View();
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "uploads");
            if (!System.IO.File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string[] allowdExtenstions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };
            string ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowdExtenstions.Contains(ext))
            {
                ViewBag.message = "invalid file";
                return View();
            }


            var name = Guid.NewGuid().ToString()+ext;
           

            var filePath = Path.Combine(folderPath, name);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            ViewBag.message = "done";
            return View();
        }
    }
}
