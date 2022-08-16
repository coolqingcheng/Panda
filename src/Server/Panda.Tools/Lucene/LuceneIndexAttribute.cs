namespace Panda.Tools.Lucene;

public class LuceneIndexAttribute : Attribute
{
    public bool IsKey { get; set; }

    public IndexType IndexType { get; set; } = IndexType.String;
}

public enum IndexType
{
    /// <summary>
    /// 会分词
    /// </summary>
    Text,
    /// <summary>
    /// 不会分词
    /// </summary>
    String
}