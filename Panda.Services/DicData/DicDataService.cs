using EasyCaching.Core;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Migrations;
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
            await _dataRepository.DeleteRange(a => a.Id == groupInfo.Id);
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

        await _caching.RemoveAsync(CacheKeys.DicDataGroupNameKey);

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
        if (groupInfo != null) await _dataRepository.DeleteRange(a => a.Pid == groupInfo.Id);
        await _dataRepository.DeleteRange(a => a.DicKey == groupName && a.Pid == 0);
    }

    public async Task<DicDataChildInfo> GetItem(string groupName, string key)
    {
        var result = await _caching.GetAsync<DicDataChildInfo>(CacheKeys.DicDataGroupNameKey + groupName + key,
            async () =>
            {
                var list = await _dataRepository.WhereItemsByGroupName(groupName);
                var item = list.Where(a => a.DicKey == key).Select(a => new DicDataChildInfo()
                {
                    Key = a.DicKey, Value = a.DicValue, Description = a.Description
                }).FirstOrDefault() ?? null;
                if (item != null) return item;
                throw new UserException($"没有找到字典组{groupName}下的{key}");
            }, TimeSpan.FromDays(1));
        if (result.HasValue)
        {
            return result.Value;
        }

        throw new UserException($"没有找到字典组{groupName}下的{key}");
    }
}

public class DicDataValidator
{
    private readonly List<DicDataRequest> _dicDataRequests = new();


    private void Init()
    {
        _dicDataRequests.Add(new(groupInfo: new DicDataGroupInfo(name: "tencent_cos", description: "腾讯云COS"),
            childInfos: new List<DicDataChildInfo>()
            {
                new(key: "cos_region", description: "COS 地域的简称"),
                new(key: "secret_id", description: "云 API 密钥 SecretId"),
                new(key: "secret_key", description: "云 API 密钥 SecretKey"),
                new(key: "bucket", description: "存储桶名称，此处填入格式必须为 bucketname-APPID"),
                new(key: "host", description: "访问默认域名")
            }));
    }

    public DicDataRequest Validate(DicUpdateRequest request)
    {
        Init();
        var dicItem = _dicDataRequests.FirstOrDefault(a => a.GroupInfo.Name == request.GroupKey);
        if (dicItem == null)
        {
            throw new UserException("groupKey不能为空");
        }

        foreach (var childInfo in dicItem.ChildInfos)
        {
            var value = request.List.FirstOrDefault(a => a.Key == childInfo.Key).Value;
            if (value == null)
            {
                throw new UserException($"配置项:{childInfo.Key}不能为空");
            }

            childInfo.Value = value;
        }

        return dicItem;
    }
}