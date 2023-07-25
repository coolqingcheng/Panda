using Microsoft.AspNetCore.Mvc;
using PandaSite.Api.Common.Models;
using PandaSite.Data.Entitys;
using PandaSite.Models;
using PandaSite.Models.Common;

namespace PandaSite.Api.Common
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
