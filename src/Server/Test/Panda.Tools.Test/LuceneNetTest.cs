
using Panda.Tools.Lucene;

namespace Panda.Tools.Test;

public class LuceneNetTest
{

}

public class TestSearchModel
{
    [LuceneIndex(IsKey = true)] public string Id { get; set; }

    [LuceneIndex(IndexType = IndexType.Text)]
    public string Title { get; set; }

    [LuceneIndex(IndexType = IndexType.Text)]
    public string Content { get; set; }
}