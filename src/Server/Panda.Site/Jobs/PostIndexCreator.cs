using Microsoft.EntityFrameworkCore;
using Panda.Admin.Services.SiteOption;
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

        private readonly ISiteOptionService _optionService;


        public PostIndexCreator(DbContext dbContext, ILogger<PostIndexCreator> logger, PostLuceneIndex postLucene, ISiteOptionService optionService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _postLucene = postLucene;
            _optionService = optionService;
        }

        public async Task Exec()
        {

            const string INDEX_TIME_UPDATE_TIME = "INDEX_TIME_UPDATE_TIME";
            _logger.LogInformation("开始执行索引创建");
            var lastTime = await _optionService.GetDateTime(INDEX_TIME_UPDATE_TIME);

            if (lastTime == null)
            {
                lastTime = await _dbContext.Set<Posts>().Where(a => a.Status == PostStatus.Publish).AsNoTracking().OrderBy(a => a.UpdateTime).Select(a => a.UpdateTime).FirstOrDefaultAsync();
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
            var dic = new Dictionary<string, string>();
            dic.Add(INDEX_TIME_UPDATE_TIME, DateTime.Now.ToString());
            await _optionService.AddOrUpdate(dic);
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
