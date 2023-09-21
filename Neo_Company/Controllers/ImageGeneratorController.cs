using Microsoft.AspNetCore.Mvc;
using Neo_Company.Models;
using SkiaSharp;

namespace Neo_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGeneratorController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GenerateImage([FromBody] SignalParams parameters)
        {
            //Задаём ширину и высоту полотна
            int widthOfCanvas = 1000;
            int heightOfCanvas = 200;

            // Создаём новый SKBitmap с требуемыми размерами
            var bitmap = new SKBitmap(widthOfCanvas, heightOfCanvas);

            // Создаём новое полотно SKCanvas из SKBitmap
            using (var canvas = new SKCanvas(bitmap))
            {
                // Рисуем синусоидальный сигнал на полотно
                SKPaint paint = new SKPaint
                {
                    Color = SKColors.Blue,
                    IsAntialias = true,
                    StrokeWidth = 5,
                };

                float x, y;
                float t = 0.0f;
                float dt = 1.0f / parameters.Fd;

                x = 0.0f;
                y = (float)(parameters.A * Math.Sin(2 * Math.PI * t * parameters.Fs));

                canvas.Clear(SKColors.White);
                canvas.DrawPoint(x, y + 100, paint);

                for (int i = 1; i < widthOfCanvas; i++)
                {
                    t += dt;
                    x += 1;
                    float newY = (float)(parameters.A * Math.Sin(2 * Math.PI * t * parameters.Fs));
                    canvas.DrawLine(x - 1, y + 100, x, newY + 100, paint);
                    y = newY;
                }
            }
    
            IActionResult fileResult;
            // Создаём новый MemoryStream чтобы держать данные SKBitmap
            using (MemoryStream stream = new MemoryStream())
            {
                // Конвертируем SKBitmap в PNG и записываем в MemoryStream
                bitmap.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);

                // Ставим позицию в MemoryStream на ноль
                stream.Position = 0;

                // Возвращайте массив байтов из MemoryStream, чтобы результат IActionResult мог интерпретировать асинхронно
                fileResult = new FileContentResult(await Task.FromResult(stream.ToArray()), "image/png")
                {
                    FileDownloadName = "sine_wave.png"
                };
            }

            return fileResult;
        }
    }
}
