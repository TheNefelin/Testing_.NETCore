using SkiaSharp;

namespace BlazorApp.Test.Utils
{
    public class ImageProcessor
    {
        private const int _quality = 65;
        private const int _targetWidth = 800;
        private const int _targetHeight = 600;

        public bool ValidateImageFormat(byte[] imageBytes)
        {
            return IsJpeg(imageBytes) || IsPng(imageBytes);
        }

        public byte[] ConvertToWebP_FromPath(string inputPath, int quality = _quality)
        {
            using var imageStream = File.OpenRead(inputPath);                       // Crear un flujo de memoria a partir de la ruta
            using var skBitmap = SKBitmap.Decode(imageStream);                      // Decodificar la imagen desde el flujo
            using var skImage = SKImage.FromBitmap(skBitmap);                       // Crear SKImage desde SKBitmap
            using var skData = skImage.Encode(SKEncodedImageFormat.Webp, quality);  // Codificar a WebP

            return skData.ToArray();                                                // Retornar los bytes del formato WebP
        }

        public byte[] ConvertToWebP_FromStream(Stream imageStream, int quality = _quality)
        {
            using var skBitmap = SKBitmap.Decode(imageStream);                      // Decodificar la imagen desde el flujo
            using var skImage = SKImage.FromBitmap(skBitmap);                       // Crear SKImage desde SKBitmap
            using var skData = skImage.Encode(SKEncodedImageFormat.Webp, quality);  // Codificar a WebP

            return skData.ToArray();                                                // Retornar los bytes del formato WebP
        }

        public byte[] ConvertToWebP_Resize_FromBytes(byte[] imageBytes, int quality = _quality, int targetWidth = _targetWidth, int targetHeight = _targetHeight)
        {
            using var imageStream = new MemoryStream(imageBytes);                               // Crear un flujo de memoria a partir del arreglo de bytes
            using var skBitmap = SKBitmap.Decode(imageStream);                                  // Decodificar la imagen desde el flujo
            using var resizedBitmap = ResizeAndCropImage(skBitmap, targetWidth, targetHeight);  // Redimensiona el liezo y la imagen
            using var skImage = SKImage.FromBitmap(resizedBitmap);                              // Crear SKImage desde SKBitmap
            using var skData = skImage.Encode(SKEncodedImageFormat.Webp, quality);              // Codificar a WebP

            return skData.ToArray();                                                            // Retornar los bytes del formato WebP
        }

        public void SaveImage(byte[] imageBytes, string outputPath)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                throw new ArgumentException("imageBytes cannot be null or empty.", nameof(imageBytes));
            }

            File.WriteAllBytes(outputPath, imageBytes);
        }

        // Redimensiona y recorta la imagen manteniendo la relación de aspecto
        private SKBitmap ResizeAndCropImage(SKBitmap bitmap, int targetWidth, int targetHeight)
        {
            float originalAspectRatio = (float)bitmap.Width / bitmap.Height;
            float targetAspectRatio = (float)targetWidth / targetHeight;
            int cropX, cropY, cropWidth, cropHeight;

            if (originalAspectRatio > targetAspectRatio)
            {
                cropWidth = (int)(bitmap.Height * targetAspectRatio);
                cropHeight = bitmap.Height;
                cropX = (bitmap.Width - cropWidth) / 2;
                cropY = 0;
            }
            else if (originalAspectRatio < targetAspectRatio)
            {
                cropWidth = bitmap.Width;
                cropHeight = (int)(bitmap.Width / targetAspectRatio);
                cropX = 0;
                cropY = (bitmap.Height - cropHeight) / 2;
            }
            else
            {
                cropWidth = targetWidth;
                cropHeight = targetHeight;
                cropX = 0;
                cropY = 0;
            }

            using var croppedBitmap = new SKBitmap(cropWidth, cropHeight);

            using (var canvas = new SKCanvas(croppedBitmap))
            {
                var srcRect = new SKRect(cropX, cropY, cropX + cropWidth, cropY + cropHeight);
                var destRect = new SKRect(0, 0, cropWidth, cropHeight);
                canvas.DrawBitmap(bitmap, srcRect, destRect);
            }

            // Redimensionar el croppedBitmap a las dimensiones objetivo
            var resizedBitmap = new SKBitmap(targetWidth, targetHeight);

            using (var canvas = new SKCanvas(resizedBitmap))
            {
                canvas.DrawBitmap(croppedBitmap, SKRect.Create(croppedBitmap.Width, croppedBitmap.Height),
                                  SKRect.Create(targetWidth, targetHeight));
            }

            return resizedBitmap;
        }

        private bool IsJpeg(byte[] imageBytes)
        {
            // JPEG header bytes: FF D8
            return imageBytes.Length >= 2 &&
                   imageBytes[0] == 0xFF &&
                   imageBytes[1] == 0xD8;
        }

        private bool IsPng(byte[] imageBytes)
        {
            // PNG header bytes: 89 50 4E 47 0D 0A 1A 0A
            return imageBytes.Length >= 8 &&
                   imageBytes[0] == 0x89 &&
                   imageBytes[1] == 0x50 &&
                   imageBytes[2] == 0x4E &&
                   imageBytes[3] == 0x47 &&
                   imageBytes[4] == 0x0D &&
                   imageBytes[5] == 0x0A &&
                   imageBytes[6] == 0x1A &&
                   imageBytes[7] == 0x0A;
        }
    }
}
