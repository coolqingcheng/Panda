using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Data.Blogs.Extensions
{
    public static class PostCateExtension
    {
        /// <summary>
        /// 保存分类
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cates"></param>
        /// <returns></returns>
        public static async Task<List<PostCates>> AddCateAsync(this DbContext context, List<string> cates)
        {
            var list = new List<PostCates>();

            foreach (var cate in cates)
            {
                var item = await context.Set<PostCates>().Where(a => a.CateName == cate).FirstOrDefaultAsync();
                if (item == null)
                {
                    item = new PostCates(cate);
                    await context.Set<PostCates>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// 添加分类关系
        /// </summary>
        /// <param name="context"></param>
        /// <param name="post"></param>
        /// <param name="cates"></param>
        /// <returns></returns>
        public static async Task PostSetCateRelation(this DbContext context, Posts post, List<PostCates> cates)
        {
            var list = await context.Set<PostCateRelation>().Where(a => a.Post == post).ToListAsync();
            if (list.Any())
            {
                context.RemoveRange(list);
                await context.SaveChangesAsync();
            }
            foreach (var item in cates)
            {
                post.CateRelations.Add(new PostCateRelation(item, post));
            }
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static async Task<List<PostCates>> GetCatesAsync(this DbContext context, List<int> ids)
        {
            var list = await context.Set<PostCates>().Where(a => ids.Contains(a.Id)).ToListAsync();
            return list;
        }
    }
}
