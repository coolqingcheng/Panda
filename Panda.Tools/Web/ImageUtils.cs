using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Panda.Tools.Web;

public class ImageUtils
{
    public static string GetPicThumbnail(byte[] bytes, int width, int height)
    {
        using var img = Image.Load(bytes);
        img.Mutate(a => a.Resize(width == 0 ? img.Width : width, height == 0 ? img.Height : height));
        return img.ToBase64String(PngFormat.Instance);
    }
}