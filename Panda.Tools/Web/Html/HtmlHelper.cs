using AngleSharp.Html.Parser;

namespace Panda.Tools.Web.Html;

public static class HtmlHelper
{
    /// <summary>
    /// 处理Html的图片为懒加载格式
    /// </summary>
    /// <param name="htmlText"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    public static string? LazyHandler(this string htmlText, string title)
    {
        var html = new HtmlParser();
        var doc = html.ParseDocument(htmlText);
        var imgs = doc.QuerySelectorAll("img");
        foreach (var img in imgs)
        {
            if (img.ClassList.Any(a => a == "lazy") == false)
            {
                img.ClassList.Add("lazy");
            }
            var src = img.GetAttribute("src");
            if (string.IsNullOrWhiteSpace(src) != false) continue;
            img.RemoveAttribute("src");
            img.SetAttribute("data-original", src);
            img.SetAttribute("alt", title);
        }

        return doc.Body?.InnerHtml;
    }

    /// <summary>
    /// 把html中的懒加载的图片attribute还原
    /// </summary>
    /// <param name="htmlText"></param>
    /// <returns></returns>
    public static string? RestoreLazyHandler(this string htmlText)
    {
        var html = new HtmlParser();
        var doc = html.ParseDocument(htmlText);
        var imgs = doc.QuerySelectorAll("img");
        foreach (var img in imgs)
        {
            var original = img.GetAttribute("data-original");
            if (string.IsNullOrWhiteSpace(original) != false) continue;
            img.RemoveAttribute("data-original");
            img.SetAttribute("src", original);
        }

        return doc.Body?.InnerHtml;
    }
}