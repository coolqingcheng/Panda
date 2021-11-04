using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Services.DicData;

public interface IDicDataService
{
    Task AddOrUpdate(DicDataRequest request);

    Task<IEnumerable<DicDataResponse>> GetDicDataList();

    Task Delete(string groupName);

    Task<DicDataChildInfo> GetItem(string groupName, string key);
}