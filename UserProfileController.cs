using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebApplication1.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public UserProfileController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Get the email ID of the currently signed-in user
                
                var userEmail = User.Identity.Name;

                // Ensure the directory for the user's images exists
                string wwwFolder = _env.WebRootPath;
                string userFolder = Path.Combine(wwwFolder, "UploadedImages", userEmail);

                // Create the directory if it doesn't exist
                if (!Directory.Exists(userFolder))
                {
                    Directory.CreateDirectory(userFolder);
                }

                // Generate a unique filename based on the email ID
                string uniqueFileName = $"{userEmail}_profile_image{Path.GetExtension(file.FileName)}";
                string filePath = Path.Combine(userFolder, uniqueFileName);

                // Save the file to the user's directory
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return RedirectToAction("UserProfile");
            }

            // Handle invalid file upload
            return RedirectToAction("Error");
        }

    }
}
