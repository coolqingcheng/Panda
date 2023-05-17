using Microsoft.AspNetCore.Mvc;
using QingCheng.Site.Api.Common.Models;
using QingCheng.Site.Data.Entitys;
using QingCheng.Site.Models;
using QingCheng.Site.Models.Common;
using QingCheng.Tools.Helper;

namespace QingCheng.Site.Api.Common
{
    /// <summary>
    /// 资源控制
    /// </summary>
    public class ResourceController : BaseAdminController
    {
        private readonly DbContext _dbcontext;
        public ResourceController(DbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        /// <summary>
        /// 获取资源文件
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageDto<ResourceModels>> Get([FromQuery] ResourceRequest req)
        {
            var query = _dbcontext.Set<SysResource>().WhereIf(req.Suffix != null, a => a.Suffix == req.Suffix);
            var res = await query.Skip(req.Skip).Take(req.PageSize).Select(a => new ResourceModels()
            {
                Url = a.Path,
                Suffix = a.Suffix,
                Domain = a.StorageDomain!,
                Size = a.Size,
                CreateTime = a.CreateTime,
                StorageType = a.StorageType
            }).ToListAsync();
            return new PageDto<ResourceModels>
            (
                await query.CountAsync(),
                res
            );
        }
    }
}
