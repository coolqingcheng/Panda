namespace Panda.Tools.Lucene;

public class LuceneIndex : Attribute
{
    public bool IsKey { get; set; }

    public IndexType IndexType { get; set; } = IndexType.String;
}

public enum IndexType
{
    Text,
    String
}