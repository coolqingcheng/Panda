using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Site.Models.FullIndexModel;
using Panda.Tools.Lucene;

namespace Panda.Site.Jobs
{
    public class PostIndexCreator
    {
        private readonly DbContext _dbContext;

        private readonly ILogger _logger;

        private readonly PostLuceneIndex _postLucene;


        public PostIndexCreator(DbContext dbContext, ILogger<PostIndexCreator> logger, PostLuceneIndex postLucene)
        {
            _dbContext = dbContext;
            _logger = logger;
            _postLucene = postLucene;
        }

        public async Task Exec()
        {
            const string INDEX_TIME_PATH = "./Content/last_index.txt";
            _logger.LogInformation("开始执行索引创建");
            if (File.Exists(INDEX_TIME_PATH) == false)
            {
                File.Create(INDEX_TIME_PATH);
            }
            var time = await File.ReadAllTextAsync(INDEX_TIME_PATH);

            if (DateTime.TryParse(time, out var lastTime) == false)
            {
                lastTime = await _dbContext.Set<Posts>().Where(a => a.Status == PostStatus.Publish).OrderByDescending(a => a.UpdateTime).Select(a => a.UpdateTime).FirstOrDefaultAsync();
            }
            _logger.LogInformation($"索引时间范围:{lastTime}");
            var list = await _dbContext.Set<Posts>().Where(a => a.UpdateTime >= lastTime && a.Status == PostStatus.Publish).AsNoTracking().Select(a => new PostFullIndexModel()
            {
                LinkId = a.CustomLink,
                Title = a.Title,
                Content = a.Text,
                LastUpdateTime = a.UpdateTime
            }).ToListAsync();
            _logger.LogInformation("索引写入条数:" + list.Count);
            _postLucene.WriteDocuments(list);
            _logger.LogInformation("写入索引完成");
            await File.WriteAllTextAsync(INDEX_TIME_PATH, DateTime.Now.ToString());
            _logger.LogInformation("修改索引更新时间完成");
        }
    }

    public class PostLuceneIndex : LuceneHelper<PostFullIndexModel>
    {
        public PostLuceneIndex() : base("Post_Index")
        {

        }
    }
}
