using AngleSharp.Html.Parser;
using Ganss.XSS;

namespace Panda.Tools.Extensions;

public static class HtmlExtension
{
    /// <summary>
    ///     获取html的text
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string GetHtmlText(this string html)
    {
        var doc = new HtmlParser().ParseDocument(html);
        return doc.Body == null ? "" : doc.Body.TextContent;
    }

    /// <summary>
    ///     获取信息摘要
    /// </summary>
    /// <param name="text"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string GetSummary(this string text, int len)
    {
        return text.Length > len ? text[..len] : text;
    }

    /// <summary>
    ///     xss过滤
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string HtmlXssFilter(this string html)
    {
        html = new HtmlSanitizer().Sanitize(html);
        return html;
    }
}