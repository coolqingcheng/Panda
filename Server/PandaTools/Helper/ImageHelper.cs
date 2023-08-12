using System.Reflection;
using SkiaSharp;

namespace PandaTools.Helper;

public class ImageHelper
{
    private static SKTypeface _embeddedTypeface;

    static ImageHelper()
    {
        if (_embeddedTypeface == null)
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(dir!, "Fonts",
                "LXGWWenKaiMono-Bold.ttf");
            using var ms = new MemoryStream(File.ReadAllBytes(path));
            _embeddedTypeface = SKTypeface.FromStream(ms);
        }
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

        using var canvas = surface.Canvas;

        canvas.DrawBitmap(bitmap, new SKRect(0, 0, info.Width, info.Height));

        using var textPaint = new SKPaint();
        textPaint.Color = new SKColor(255, 255, 255, 180);
        textPaint.TextSize = fontSize;
        textPaint.TextAlign = SKTextAlign.Center;

        textPaint.Typeface = _embeddedTypeface;
        textPaint.IsAntialias = true;
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
        return data.ToArray();
    }
}