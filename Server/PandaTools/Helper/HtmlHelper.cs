using AngleSharp.Html.Parser;

namespace PandaTools.Helper;

public static class HtmlHelper
{
    public static string? GetHtmlContent(string html, int len)
    {
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(html);
        return doc.Body?.TextContent.RemoveEmpty();
    }
}