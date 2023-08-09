using SkiaSharp;

namespace PandaTools.Helper;

public class ImageHelper
{
    /// <summary>
    /// 压缩并裁切图片去除上下水印
    /// </summary>
    /// <param name="source">原文件位置</param>
    /// <param name="target">生成目标文件位置</param>
    /// <param name="maxWidth">最大宽度，根据此宽度计算是否需要缩放，计算新高度</param>
    /// <param name="top">顶部裁切高度，单位px</param>
    /// <param name="bottom">底部裁切高度，单位px</param>
    /// <param name="quality">图片质量，范围0-100</param>
    public static void CompressAndCut(string source, string target, decimal maxWidth, int top, int bottom, int quality)
    {
        using var file = File.OpenRead(source);
        using var fileStream = new SKManagedStream(file);
        using var bitmap = SKBitmap.Decode(fileStream);
        var width = (decimal)bitmap.Width;
        var height = (decimal)bitmap.Height;
        var newWidth = width;
        var newHeight = height;
        if (width > maxWidth)
        {
            newWidth = maxWidth;
            newHeight = height / width * maxWidth;
        }

        using var resized = bitmap.Resize(new SKImageInfo((int)newWidth, (int)newHeight), SKFilterQuality.Medium);
        if (resized == null) return;
        using var surface = SKSurface.Create(new SKImageInfo((int)newWidth, (int)newHeight - top - bottom));
        var canvas = surface.Canvas;
        canvas.DrawBitmap(resized, 0, 0 - top);
        using var image = surface.Snapshot();
        using var writeStream = File.OpenWrite(target);
        image.Encode(SKEncodedImageFormat.Jpeg, quality).SaveTo(writeStream);
    }
    
    /// <summary>
    /// 绘制水印
    /// </summary>
    /// <param name="imageBytes">图片</param>
    /// <param name="watermarkText">水印文字</param>
    /// <returns></returns>
    public static byte[] AddWatermarkToImage(byte[] imageBytes, string watermarkText)
    {
        using MemoryStream imageStream = new MemoryStream(imageBytes);
        using SKBitmap originalBitmap = SKBitmap.Decode(imageStream);
         SKImageInfo info = originalBitmap.Info; // Remove "using" here

        using SKSurface surface = SKSurface.Create(info);
        SKCanvas canvas = surface.Canvas;

        // Draw the original image
        canvas.DrawBitmap(originalBitmap, new SKRect(0, 0, info.Width, info.Height));

        // Create a SKPaint object for the watermark text
        SKPaint textPaint = new SKPaint
        {
            Color = SKColors.White,
            TextSize = 40,
            TextAlign = SKTextAlign.Center,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal)
        };

        // Draw the watermark text
        canvas.DrawText(watermarkText, info.Width / 2, info.Height - 50, textPaint);

        // Convert the SKSurface to a byte array
                   
        // Convert the SKSurface to SKImage
        SKImage image = surface.Snapshot();

        // Convert the SKImage to SKData (encoded image)
        using SKData encodedData = image.Encode();
        // Convert SKData to byte array
        byte[] encodedBytes = encodedData.ToArray();
        return encodedBytes;
    }
}