namespace PandaSite.Services.Blogs
{
    public class PostCommentService
    {
        private readonly DbContext _context;

        public PostCommentService(DbContext context)
        {
            _context = context;
        }


    }
}
