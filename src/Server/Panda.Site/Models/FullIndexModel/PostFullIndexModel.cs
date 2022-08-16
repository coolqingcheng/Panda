using Panda.Tools.Lucene;

namespace Panda.Site.Models.FullIndexModel
{
    /// <summary>
    /// 文章索引
    /// </summary>
    public class PostFullIndexModel
    {
        [LuceneIndex(IndexType = IndexType.String, IsKey = true)]
        public string LinkId { get; set; }

        [LuceneIndex(IndexType = IndexType.Text)]
        public string Title { get; set; }

        [LuceneIndex(IndexType = IndexType.Text)]
        public string Content { get; set; }

        [LuceneIndex(IndexType = IndexType.String)]
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [LuceneIndex(IndexType = IndexType.String)]
        public string ThumbImg { get; set; }
    }
}
