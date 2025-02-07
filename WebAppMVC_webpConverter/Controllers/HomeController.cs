using ClassLibrary.Utils.Utils;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using System.Diagnostics;
using WebAppMVC_webpConverter.Models;

namespace WebAppMVC_webpConverter.Controllers
{
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
            ViewBag.Quality1 = 65;
            ViewBag.Quality2 = 65;

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
            List<Imagen> imageList = new List<Imagen>();

            var uploadPath = Path.Combine(_environment.WebRootPath, "Images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var newImage in newImages)
            {
                string imageWebP = Path.ChangeExtension(newImage.FileName, ".webp");
                string outputPath = Path.Combine(uploadPath, imageWebP);

                using (var imageStream = newImage.OpenReadStream())
                {
                    var imageBytes = imageProcessor.ConvertToWebP_FromStream(imageStream);
                    imageProcessor.SaveImage(imageBytes, outputPath);
                }

                // ClasicConvert
                //using (var imageStream = imagen.OpenReadStream())
                //ClasicConvert(streamEntrada, rutaImagenWebP);

                // Agregar la información de la imagen a la lista
                imageList.Add(new Imagen
                {
                    NomImagen = imageWebP,
                    RutaImagen = "/Images/" + imageWebP,
                });
            }

            return View(imageList);
        }

        private void ClasicConvert(Stream streamEntrada, string rutaImagenWebP)
        {
            // Convertir a formato WebP y guardar la imagen
            using (var skiaImagen = SKBitmap.Decode(streamEntrada))
            using (var skiaImagenWebP = SKImage.FromBitmap(skiaImagen))
            using (var datosWebP = skiaImagenWebP.Encode(SKEncodedImageFormat.Webp, 65)) // Calidad de 65
            using (var streamSalida = new FileStream(rutaImagenWebP, FileMode.Create))
            {
                datosWebP.SaveTo(streamSalida);
            }
        }

    }
}
