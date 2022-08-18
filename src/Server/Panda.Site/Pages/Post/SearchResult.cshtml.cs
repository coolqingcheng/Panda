using Microsoft.AspNetCore.Mvc.RazorPages;
using Panda.Site.Jobs;
using Panda.Site.Models;
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

    public int Count { get; set; }

    public int CurrIndex { get; set; }

    public List<PostListItem> ListItems { get; set; }

    public void OnGet(string keyword, int index = 1)
    {
        var result = _postLucene.Search(keyword, index, 10);
        CurrIndex = index;
        Count = result.Total;
        ListItems = new List<PostListItem>();
        foreach (var item in result.list)
        {
            ListItems.Add(new PostListItem()
            {
                Title = item.Title,
                Summay = item.Content,
                UpdateTime = item.LastUpdateTime,
                Link = item.LinkId
            });
        }

    }
}