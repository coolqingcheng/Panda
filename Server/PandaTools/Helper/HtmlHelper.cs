using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaTools.Helper
{
    public static class HtmlHelper
    {

        public static string? GetHtmlContent(string html, int len)
        {
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(html);
            return doc.Body?.TextContent.RemoveEmpty();
        }
    }
}
