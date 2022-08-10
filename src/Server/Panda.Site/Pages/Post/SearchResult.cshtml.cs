using Microsoft.AspNetCore.Mvc.RazorPages;
using Panda.Tools.Lucene;

namespace Panda.Site.Pages.Post;

public class SearchResult : PageModel
{
    
    public void OnGet(string keyword)
    {
        // _luceneHelper.WriteDocument("http://www.baidu.com","百度一下","百度是最大的中文搜索引擎");
        // _luceneHelper.WriteDocument("http://www.baidu.com/1","百度一下","百度是最大的中文搜索引擎");
        // _luceneHelper.Search("百度");
    }
}