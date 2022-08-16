namespace Panda.Tools.Lucene;

public class LuceneIndexAttribute : Attribute
{
    public bool IsKey { get; set; }

    public IndexType IndexType { get; set; } = IndexType.String;
}

public enum IndexType
{
    /// <summary>
    /// ��ִ�
    /// </summary>
    Text,
    /// <summary>
    /// ����ִ�
    /// </summary>
    String
}