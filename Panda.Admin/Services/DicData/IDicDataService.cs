using Panda.Admin.Models.Request;
using Panda.Entity.Responses;

namespace Panda.Admin.Services.DicData;

public interface IDicDataService
{
    Task AddOrUpdate(DicDataRequest request);

    Task<IEnumerable<DicDataResponse>> GetDicDataList();

    Task Delete(string groupName);

    /// <summary>
    ///     获取配置项目，可能是缓存中获取
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<DicDataChildInfo?> GetItemByCache(string groupName, string key);


    Task<IEnumerable<DicDataChildInfo>> GetItemByGroupName(string groupName);

    Task<string?> GetItemByCache(string section);
}