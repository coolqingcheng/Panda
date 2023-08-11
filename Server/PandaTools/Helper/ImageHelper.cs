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
    /// 图片文字水印
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="text"></param>
    /// <param name="fontSize"></param>
    /// <returns></returns>
    public static byte[] WriteWaterTag(byte[] bytes, string text, int fontSize)
    {
        using var ms = new MemoryStream(bytes);

        using var bitmap = SKBitmap.Decode(ms);
        var info = bitmap.Info;

        using var surface = SKSurface.Create(info);

        var canvas = surface.Canvas;

        canvas.DrawBitmap(bitmap, new SKRect(0, 0, info.Width, info.Height));

        using var textPaint = new SKPaint();
        textPaint.Color = new SKColor(255, 255, 255, 180);
        textPaint.TextSize = fontSize;
        textPaint.TextAlign = SKTextAlign.Center;
        textPaint.Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal);
        var textWidth = textPaint.GetGlyphWidths(text).Sum();

        SKFontMetrics fontMetrics = textPaint.FontMetrics;
        float textHeight = fontMetrics.CapHeight; //fontMetrics.Bottom - fontMetrics.Top;

        using var rectPaint = new SKPaint();
        rectPaint.Color = new SKColor(0, 0, 0, 100);

        //计算水印背景色的宽高和位置
        var bgW = textWidth += textWidth / 3;
        var bgH = textHeight += textHeight;
        var bgX = info.Width - bgW - 20;
        var bgY = info.Height - bgH - 20;

        canvas.DrawRect(bgX, bgY, bgW, bgH, rectPaint);

        //根据背景色的位置，计算文字的位置s
        var textX = bgX + textWidth / 2;

        var textY = bgY + textHeight / 4 * 3;
        // Draw the watermark text
        canvas.DrawText(text, textX, textY, textPaint);


        using var data = surface.Snapshot().Encode(SKEncodedImageFormat.Jpeg, 80);

        var dataBytes = data.ToArray();
        return dataBytes;
    }
}