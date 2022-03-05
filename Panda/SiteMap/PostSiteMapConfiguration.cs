using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Panda.Entity.DataModels;
using SimpleMvcSitemap;

namespace Panda.SiteMap;

public class PostSiteMapConfiguration : SitemapIndexConfiguration<Posts>
{
    public PostSiteMapConfiguration(IQueryable<Posts> dataSource, int? currentPage) : base(
        dataSource, currentPage)
    {
    }

    public override SitemapIndexNode CreateSitemapIndexNode(int currentPage)
    {
        return new SitemapIndexNode("postSiteMap_" + currentPage);
    }

    public override SitemapNode CreateNode(Posts source)
    {
        var node = new SitemapNode($"/post/{source.CustomLink}.html")
        {
            LastModificationDate = source.AddTime.LocalDateTime
        };

        return node;
    }
}