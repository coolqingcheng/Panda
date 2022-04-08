using System.Xml;

namespace Panda.Tools.Web.RSS;

public class RssUtils
{
    public static string ToRssXml(RssModel model)
    {
        var doc = new XmlDocument();
        var version = doc.CreateXmlDeclaration("1.0", "utf-8", null);
        doc.AppendChild(version);
        var rss = doc.CreateElement("rss");
        rss.SetAttribute("version", "2.0");
        doc.AppendChild(rss);
        var channel = doc.CreateElement("channel");
        rss.AppendChild(channel);
        var title = doc.CreateElement("title");
        title.InnerText = model.Title;
        channel.AppendChild(title);
        var link = doc.CreateElement("link");
        channel.AppendChild(link);
        link.InnerText = model.Link;
        var description = doc.CreateElement("description");
        channel.AppendChild(description);
        description.AppendChild(doc.CreateCDataSection(model.Description));
        model.Item.ForEach(a =>
        {
            var item = doc.CreateElement("item");
            channel.AppendChild(item);
            var itemTitle = doc.CreateElement("title");
            item.AppendChild(itemTitle);
            itemTitle.InnerText = a.Title;

            var itemLink = doc.CreateElement("link");
            item.AppendChild(itemLink);
            itemLink.InnerText = a.Link;

            var itemDescription = doc.CreateElement("description");
            item.AppendChild(itemDescription);
            itemDescription.AppendChild(doc.CreateCDataSection(a.Description));
        });

        return doc.InnerXml;
    }
}