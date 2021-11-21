using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Panda.Tools.Web;

public class ImageUtils
{
    public static string GetPicThumbnail(byte[] bytes, int width, int height)
    {
        var img = Image.Load(bytes);
        img.Mutate(a => a.Resize(width, height));
        return img.ToBase64String(JpegFormat.Instance);
    }
}