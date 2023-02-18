using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QingCheng.Site.Api.Blogs.Services.Models;
using QingCheng.Site.Data.Blogs;
using QingCheng.Site.Models;

namespace QingCheng.Site.Api.Blogs.Services
{
    public class PostCateService
    {
        private readonly QingChengContext _context;
        public PostCateService(QingChengContext context)
        {
            _context = context;
        }

        public async Task<PageDto<CateDtoModel>> GetList(BasePageModel model)
        {
            var query = _context.PostCates;
            var list = await query.Skip(model.Skip).Take(model.PageSize)
                .Include(a => a.PostCateRelations)
                .Select(a => new CateDtoModel()
                {
                    Id = a.Id,
                    CateName = a.CateName,
                    CreateTime = a.CreateTime,
                    LastUpdateTime = a.UpdateTime,
                    PostCount = a.PostCateRelations.Count()
                }).ToListAsync();
            return new PageDto<CateDtoModel>(await query.CountAsync(), list);
        }

        public async Task Edit(CateRequest request)
        {
            if (request.Id>0)
            {
                var item = await _context.PostCates.FirstOrDefaultAsync(a => a.Id == request.Id);
                if (item == null)
                {
                    throw new UserException("找不到更新的分类");
                }

                item.CateName = request.CateName;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.PostCates.AddAsync(new PostCates(request.CateName));
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int Id)
        {
            var item = await _context.PostCates.Include(a => a.PostCateRelations).Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (item is { PostCateRelations.Count: > 0 })
            {
                throw new UserException("分类下为空，不能删除");
            }
            await _context.PostCates.Where(a => a.Id == Id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

    }
}
