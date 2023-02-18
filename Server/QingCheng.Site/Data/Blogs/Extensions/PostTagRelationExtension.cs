using Microsoft.EntityFrameworkCore;
using QingCheng.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Data.Blogs.Extensions
{
    public static class PostTagRelationExtension
    {
        /// <summary>
        /// 移除标签关系
        /// </summary>
        /// <param name="context"></param>
        /// <param name="post"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static async Task PostRemoveTagRelation(this DbContext context, Posts post, List<PostTags>? tags = null)
        {
            var list = await context.Set<PostTagRelation>().Where(a => a.Post == post)
                .WhereIf(tags != null, a => tags!.Contains(a.PostTags))
                .ToListAsync();
            await context.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// 文章添加标签关系
        /// </summary>
        /// <param name="context"></param>
        /// <param name="post"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static async Task PostSetTagRelationAsync(this DbContext context, Posts post, List<PostTags> tags)
        {
            var list = await context.Set<PostTagRelation>().Where(a => tags.Contains(a.PostTags)).ToListAsync();
            context.RemoveRange(list);
            await context.SaveChangesAsync();
            await context.PostRemoveTagRelation(post, tags!);
            await context.Set<PostTagRelation>().AddRangeAsync(tags.Select(a => new PostTagRelation(post, a)));
            await context.SaveChangesAsync();
        }

    }

    public static class TagExtension
    {
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static async Task<List<PostTags>> GetTagsAsync(this DbContext context, List<string> tags)
        {
            var list = new List<PostTags>();
            foreach (var tag in tags)
            {
                var item = await context.Set<PostTags>().Where(a => a.TagName == tag).FirstOrDefaultAsync();
                if (item == null)
                {
                    item = new PostTags(tag);
                    await context.Set<PostTags>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
                list.Add(item);
            }
            return list;
        }
    }
}
