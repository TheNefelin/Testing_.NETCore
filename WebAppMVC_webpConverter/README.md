# .webp Converter WebApp (Model-View-Controller)

### Dependencies
```
SkiaSharp
```

## Model
```
public class Imagen
{
    public string NomImagen { get; set; }
    public string RutaImagen { get; set; }
    public string WebPImagen { get; set; }
}
```

## Controller
```
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
```

## View
```
<form method="post" enctype="multipart/form-data">
    <h2>Convertir a WebP</h2>
    <input id="imagenes" name="imagenes" type="file" multiple accept="image/png, image/jpeg" />
    <input id="uploadButton" type="submit" value="upload" />
    <div id="progress-container" style="display:none;">
        <progress id="progress-bar" max="100" value="0"></progress>
    </div>
</form>


@if (Model != null && Model.Count > 0)
{
    <h3>Imágenes Cargadas:</h3>
    <ul>
        @foreach (var imagen in Model)
        {
            <li>
                <img src="@Url.Content(imagen.RutaImagen)"
                     alt="@imagen.NomImagen"
                     style="height: 250px; margin-bottom: 4px" />
            </li>
        }
    </ul>
}
```

