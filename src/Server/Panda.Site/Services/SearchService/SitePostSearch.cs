using Panda.Tools.Lucene;

namespace Panda.Site.Services.SearchService;

public class SiteSearch : LuceneHelper
{
    public SiteSearch() : base("Post_Index")
    {
    }
}