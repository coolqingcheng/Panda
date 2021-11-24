using EasyCaching.Core;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Entity.UnitOfWork;
using Panda.Repository.DicData;
using Panda.Tools.Exception;

namespace Panda.Services.DicData;

public class DicDataService : IDicDataService
{
    private readonly DicDataRepository _dataRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IEasyCachingProvider _caching;

    public DicDataService(DicDataRepository dataRepository, IUnitOfWork unitOfWork, IEasyCachingProvider caching)
    {
        _dataRepository = dataRepository;
        _unitOfWork = unitOfWork;
        _caching = caching;
    }

    public async Task AddOrUpdate(DicDataRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        var groupInfo = await _dataRepository.Where(a => a.DicKey == request.GroupInfo.Name && a.Pid == 0)
            .FirstOrDefaultAsync();
        if (groupInfo == null)
        {
            groupInfo = new DicDatas()
            {
                DicKey = request.GroupInfo.Name,
                DicValue = "-",
                Description = request.GroupInfo.Description
            };
            await _dataRepository.AddAsync(groupInfo);
        }
        else
        {
            await _dataRepository.DeleteWhere(a => a.Pid == groupInfo.Id);
        }

        foreach (var item in request.ChildInfos)
        {
            await _dataRepository.AddAsync(new DicDatas()
            {
                DicKey = item.Key,
                DicValue = item.Value,
                Description = item.Description,
                Pid = groupInfo.Id
            });
        }

        await _caching.RemoveByPrefixAsync(CacheKeys.DicDataGroupNameKey);

        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<DicDataResponse>> GetDicDataList()
    {
        var list = (await _dataRepository.GetAll()).ToList();
        return list.Where(a => a.Pid == 0).Select(a => new DicDataResponse()
        {
            GroupInfo = new DicDataGroupInfo()
            {
                Name = a.DicKey,
                Description = a.Description
            },
            ChildInfo = list.Where(b => b.Pid == a.Id).Select(b => new DicDataChildInfo()
            {
                Key = b.DicKey,
                Value = b.DicValue,
                Description = b.Description
            })
        });
    }

    public async Task Delete(string groupName)
    {
        var groupInfo = await _dataRepository.Where(a => a.DicKey == groupName && a.Pid == 0).FirstOrDefaultAsync();
        if (groupInfo != null) await _dataRepository.DeleteWhere(a => a.Pid == groupInfo.Id);
        await _dataRepository.DeleteWhere(a => a.DicKey == groupName && a.Pid == 0);
    }

    public async Task<DicDataChildInfo?> GetItemByCache(string groupName, string key)
    {
        var result = await _caching.GetAsync(CacheKeys.DicDataGroupNameKey + groupName + key,
            async () =>
            {
                var list = await _dataRepository.WhereItemsByGroupName(groupName);
                var item = list.Where(a => a.DicKey == key).Select(a => new DicDataChildInfo()
                {
                    Key = a.DicKey,
                    Value = a.DicValue,
                    Description = a.Description
                }).FirstOrDefault();
                return item;
            }, TimeSpan.FromDays(1));
        return result.HasValue ? result.Value : null;
    }

    public async Task<IEnumerable<DicDataChildInfo>> GetItemByGroupName(string groupName)
    {
        var groupItems = await _dataRepository.WhereItemsByGroupName(groupName);

        return groupItems.Select(a => new DicDataChildInfo(a.DicKey, a.Description)
        {
            Value = a.DicValue
        });
    }

    public async Task<string?> GetItemByCache(string section)
    {
        var arr = section.Split(":");
        if (arr.Length == 0) throw new UserException("section获取字典内容格式必须用:分割");
        var item = await GetItemByCache(arr[0], arr[1]);
        return item?.Value;
    }
}