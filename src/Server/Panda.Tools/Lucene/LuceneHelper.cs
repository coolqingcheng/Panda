using System.Text;
using jieba.NET;
using JiebaNet.Segmenter;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.Linq;
using System.Reflection;
using Lucene.Net.Search.Highlight;
using Panda.Tools.Extensions;

namespace Panda.Tools.Lucene;

public abstract class LuceneHelper<T> : IDisposable where T : class, new()
{
    private readonly IndexWriter _writer;
    private readonly Analyzer _analyzer;

    public LuceneHelper(string indexPath = "Default_Index_Data")
    {
        var dir = FSDirectory.Open($"Lucene_Index/{indexPath}");
        _analyzer = new JieBaAnalyzer(TokenizerMode.Search);

        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer);
        _writer = new IndexWriter(dir, indexConfig);
    }

    /// <summary>
    /// 写入文档
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    public virtual void WriteDocuments<T>(IEnumerable<T> list) where T : class, new()
    {
        foreach (var item in list)
        {
            var properties = item.GetType().GetProperties();
            var key = properties.FirstOrDefault(a => a.GetCustomAttribute<LuceneIndexAttribute>()?.IsKey == true);
            if (key == null)
            {
                throw new ArgumentException($"{typeof(T).Name}没有key");
            }

            _writer.DeleteDocuments(new Term(key.Name, item.GetPropertyValue(key.Name).ToString()));


            var doc = new Document();
            foreach (var property in properties)
            {
                var att = property.GetCustomAttribute<LuceneIndexAttribute>();
                if (att == null)
                {
                    continue;
                }
                var name = property.Name;
                var value = item.GetPropertyValue(name);
                if (value == null)
                {
                    value = "";
                }
                if (att is { IndexType: IndexType.String })
                {
                    doc.Add(new StringField(name, value.ToString(), Field.Store.YES));
                }

                if (att is { IndexType: IndexType.Text })
                {
                    doc.Add(new TextField(name, value.ToString(), Field.Store.YES));
                }
            }

            _writer.AddDocument(doc);
        }
        _writer.Flush(triggerMerge: true, applyAllDeletes: true);
        _writer.Commit();
    }

    private void WriteDocument(string url, string title, string description)
    {
        _writer.DeleteDocuments(new Term("url", url));

        Document doc = new();
        doc.Add(new StringField("url", url, Field.Store.YES));

        TextField titleField = new TextField("title", title, Field.Store.YES);
        titleField.Boost = 2F;

        TextField descriptionField = new TextField("description", description, Field.Store.YES);
        descriptionField.Boost = 1F;

        doc.Add(titleField);
        doc.Add(descriptionField);
        _writer.AddDocument(doc);
        _writer.Flush(triggerMerge: true, applyAllDeletes: true);
        _writer.Commit();
    }

    private static IEnumerable<string> GetKeyWords(string q)
    {
        var segmenter = new JiebaSegmenter();
        var result = segmenter.CutForSearch(q);
        return result;
    }

    public void DeleteAll()
    {
        _writer.DeleteAll();
        _writer.Flush(triggerMerge: true, applyAllDeletes: true);
        _writer.Commit();
    }

    public class SearchResult<T>
    {
        public int Total { get; set; }

        public List<T> list { get; set; }
    }

    public virtual SearchResult<T> Search(string q, int pageIndex, int pageSize)
    {
        var reader = _writer.GetReader(applyAllDeletes: true);
        var searcher = new IndexSearcher(reader);

        var query = new BooleanQuery();
        var type = typeof(T);
        foreach (var item in GetKeyWords(q))
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                var att = propertyInfo.GetCustomAttribute<LuceneIndexAttribute>();

                if (att is { IndexType: IndexType.Text })
                {
                    query.Add(new TermQuery(new Term(propertyInfo.Name, item)), Occur.SHOULD);
                }
            }
        }

        var start = (pageIndex - 1) * pageSize;

        #region 代码高亮

        // 1.建立分值对象(QueryScorer), 用于对高亮显示内容打分
        QueryScorer qs = new QueryScorer(query);
        // 2.建立输出片段对象(Fragmenter), 用于把高亮显示内容切片
        var fragmenter = new SimpleSpanFragmenter(qs);
        // 3.建立高亮组件对象(Highlighter), 实现高亮显示
        // 3.1.实现自定义的HTML标签进行高亮显示搜索结果
        // 1) 建立高亮显示HTML格式化标签对象(SimpleHTMLFormatter), 参数说明: 
        // preTag: 指定HTML标签的开始部分(<font color='red'>)
        // postTag: 指定HTML标签的结束部分(</font>)
        SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<font color='red'>", "</font>");
        // 2) 指定高亮显示组件对象（Highter）, 使用SimpleHTMLFormatter对象
        Highlighter lighter = new Highlighter(formatter, qs);
        // 设置切片对象
        lighter.TextFragmenter = fragmenter;

        #endregion


        var docs = searcher.Search(query, pageSize);
        if (docs.TotalHits > pageSize)
        {
            var index = start - 1;
            ScoreDoc preScore = docs.ScoreDocs[index < 0 ? 0 : index];
            docs = searcher.SearchAfter(preScore, query, pageSize);
        }

        var hits = docs.ScoreDocs;

        var list = new List<T>();

        foreach (var hit in hits)
        {
            var document = searcher.Doc(hit.Doc);
            var t = new T();
            foreach (var propertyInfo in t.GetType().GetProperties())
            {
                var value = document.Get(propertyInfo.Name);
                if (propertyInfo.GetCustomAttribute<LuceneIndexAttribute>()?.IndexType == IndexType.Text)
                {
                    var lighterText = lighter.GetBestFragment(_analyzer, propertyInfo.Name, value);
                    if (string.IsNullOrWhiteSpace(lighterText) == false)
                    {
                        value = lighterText;
                    }
                }

                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    t.SetPropertyValue(propertyInfo.Name, value);
                }
            }
            list.Add(t);
        }
        return new SearchResult<T>()
        {
            Total = docs.TotalHits,
            list = list
        };
    }

    public void Dispose()
    {
        _writer?.Dispose();
    }
}