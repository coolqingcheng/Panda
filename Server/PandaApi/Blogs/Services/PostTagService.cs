namespace PandaApi.Blogs.Services
{
    public class PostTagService
    {
        private readonly QingChengContext _context;

        public PostTagService(QingChengContext context)
        {
            _context = context;
        }

        public async Task<PageDto<TagDtoModel>> GetList(TagListRequest request)
        {
            var query = _context.PostTags.AsQueryable();
            var list = await query.Skip(request.Skip).Take(request.PageSize)
                 .Include(a => a.TagRelation)
                 .Select(a => new TagDtoModel()
                 {
                     Id = a.Id,
                     TagName = a.TagName,
                     Count = a.TagRelation.Count()
                 }).ToListAsync();
            return new PageDto<TagDtoModel>(await query.CountAsync(), list);
        }
    }
}
