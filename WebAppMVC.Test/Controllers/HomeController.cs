using System.Diagnostics;
using ClassLibrary.Utils.Models;
using ClassLibrary.Utils.Utils;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Test.Models;

namespace WebAppMVC.Test.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    public IActionResult Index(List<IFormFile> newImages)
    {
        var imageProcessor = new ImageProcessor();
        List<UploadImage> imageList = new List<UploadImage>();

        var uploadPath = Path.Combine(_environment.WebRootPath, "WebP");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        foreach (var newImage in newImages)
        {
            string imageWebP = Path.ChangeExtension(newImage.FileName, ".webp");
            string outputPath = Path.Combine(uploadPath, imageWebP);
            var uploadImages = new UploadImage()
            {
                ImageName = imageWebP,
                ImageLocalPath = "/WebP/" + imageWebP,
            };

            using (var imageStream = newImage.OpenReadStream())
            {
                DataImage dataImage = imageProcessor.ConvertToWebP_FromStream_AndSave(imageStream, outputPath);

                uploadImages.OriginalWidth = dataImage.OriginalWidth;
                uploadImages.OriginalHeight = dataImage.OriginalHeight;
                uploadImages.OriginalAspectRatio = dataImage.OriginalAspectRatio;
                uploadImages.TargetWidth = dataImage.TargetWidth;
                uploadImages.TargetHeight = dataImage.TargetHeight;
                uploadImages.TargetAspectRatio = dataImage.TargetAspectRatio;
            }

            imageList.Add(uploadImages);
        }

        return View(imageList);
    }
}
