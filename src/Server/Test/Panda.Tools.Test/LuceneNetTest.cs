using Panda.Site.Services.SearchService;
using Panda.Tools.Lucene;

namespace Panda.Tools.Test;

public class LuceneNetTest
{
    [Test]
    public void CreateIndex()
    {
        new SiteSearch()
            .WriteDocument(new TestSearchModel()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "百度搜索引擎",
                Content = "百度是一个中文的搜索引擎"
            });
    }

    [Test]
    public void TestSearch()
    {
        var list = new SiteSearch().Search<TestSearchModel>("我寻你千百度", 1, 10).ToList();
        foreach (var item in list)
        {
            Console.WriteLine($"Id{item.Id} content: {item.Content} title: {item.Title}");
        }
    }
}

public class TestSearchModel
{
    [LuceneIndex(IsKey = true)] public string Id { get; set; }

    [LuceneIndex(IndexType = IndexType.Text)]
    public string Title { get; set; }

    [LuceneIndex(IndexType = IndexType.Text)]
    public string Content { get; set; }
}