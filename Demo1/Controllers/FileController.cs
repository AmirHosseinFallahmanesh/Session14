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
            if (name.Contains(".."))
            {
                return BadRequest();
            }
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

    }
}
