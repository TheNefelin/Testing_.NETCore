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
        public IActionResult Index(List<IFormFile> imagenes)
        {
            List<Imagen> listaImagenes = new List<Imagen>();

            foreach (var imagen in imagenes)
            {
                // Cambia la extensión de la imagen a .webp
                string webpImagen = Path.GetFileNameWithoutExtension(imagen.FileName) + ".webp";

                // Define la ruta donde se guardará la imagen en formato WebP en la carpeta Images
                string rutaImagenWebP = Path.Combine(_environment.WebRootPath, "Images", webpImagen);

                // Crear directorio si no existe
                string carpetaImagenes = Path.GetDirectoryName(rutaImagenWebP);
                if (!Directory.Exists(carpetaImagenes))
                {
                    Directory.CreateDirectory(carpetaImagenes);
                }

                // Convertir a formato WebP y guardar la imagen
                using (var streamEntrada = imagen.OpenReadStream())
                using (var skiaImagen = SKBitmap.Decode(streamEntrada))
                using (var skiaImagenWebP = SKImage.FromBitmap(skiaImagen))
                using (var datosWebP = skiaImagenWebP.Encode(SKEncodedImageFormat.Webp, 65)) // Calidad de 65
                using (var streamSalida = new FileStream(rutaImagenWebP, FileMode.Create))
                {
                    datosWebP.SaveTo(streamSalida);
                }

                // Agregar la información de la imagen a la lista
                listaImagenes.Add(new Imagen
                {
                    NomImagen = webpImagen,
                    RutaImagen = "/Images/" + webpImagen,
                });
            }

            return View(listaImagenes);
        }

    }
}
