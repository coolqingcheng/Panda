using Microsoft.AspNetCore.Mvc.RazorPages;
using Panda.Site.Jobs;
using Panda.Site.Models.FullIndexModel;
using Panda.Tools.Lucene;

namespace Panda.Site.Pages.Post;

public class SearchResult : PageModel
{
    private readonly PostLuceneIndex _postLucene;

    public SearchResult(PostLuceneIndex postLucene)
    {
        _postLucene = postLucene;
    }

    public int Total { get; set; }

    public List<PostFullIndexModel> list { get; set; }

    public void OnGet(string keyword, int index, int size)
    {
        var result = _postLucene.Search(keyword, index, size);
        
    }
}