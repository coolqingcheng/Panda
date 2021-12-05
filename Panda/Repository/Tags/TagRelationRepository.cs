using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Tags;

public class TagRelationRepository : PandaRepository<TagsRelation>
{
    public TagRelationRepository(PandaContext context) : base(context)
    {
    }

    public async Task PostDeleteRelationAsync(Posts post)
    {
        var relations = await _context.TagsRelations.Include(a => a.Tags).Where(a => a.Posts == post).ToListAsync();
        foreach (var relation in relations)
        {
            _context.TagsRelations.Remove(relation);
            relation.Tags.PostCount -= 1;
        }

        await _context.SaveChangesAsync();
    }

    public async Task AddRelationAsync(Posts post, PostTags tag)
    {
        await _context.TagsRelations.AddAsync(new TagsRelation()
        {
            Posts = post,
            Tags = tag
        });
        tag.PostCount += 1;
        await _context.SaveChangesAsync();
    }
}