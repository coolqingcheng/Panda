using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Tags;

public class PostTagsRepository:PandaRepository<PostTags>
{
    public PostTagsRepository(PandaContext context) : base(context)
    {
    }

    public async Task<PostTags> GetWithCreate(string tagName)
    {
       var tagItem = await  Where(a => a.TagName == tagName).FirstOrDefaultAsync();
       if (tagItem == null)
       {
           tagItem = new PostTags()
           {
               TagName = tagName
           };
            await AddAsync(tagItem);
       }

       return tagItem;
    }
}