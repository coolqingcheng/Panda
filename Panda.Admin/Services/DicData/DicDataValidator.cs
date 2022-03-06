using Panda.Admin.Models.Request;
using Panda.Tools.Exception;

namespace Panda.Services.DicData;

public class DicDataValidator
{
    private readonly List<DicDataRequest> _dicDataRequests = new();


    private void Init()
    {
        _dicDataRequests.Add(new DicDataRequest(
            groupInfo: new DicDataGroupInfo(name: "tencent_cos", description: "腾讯云COS"),
            childInfos: new List<DicDataChildInfo>()
            {
                new(key: "cos_region", description: "COS 地域的简称"),
                new(key: "secret_id", description: "云 API 密钥 SecretId"),
                new(key: "secret_key", description: "云 API 密钥 SecretKey", true),
                new(key: "bucket", description: "存储桶名称，此处填入格式必须为 bucketname-APPID"),
                new(key: "host", description: "访问默认域名")
            }));
        _dicDataRequests.Add(new DicDataRequest(new DicDataGroupInfo("site", "网站设置"), new List<DicDataChildInfo>()
        {
            new("site_name", "网站名称"),
            new("icp", "备案号", isRequired: false),
            new("site_description","网站描述",isRequired:true),
            new("statistics", "统计代码", isRequired: false),
            new("img_lazy", "图片是否开启懒加载", isRequired: false),
            new("host", "网站域名")
        }));
        _dicDataRequests.Add(new DicDataRequest(new DicDataGroupInfo("wechat", "desc"),
            childInfos: new List<DicDataChildInfo>()
            {
                new("wechat_official_account_desc", "公众号说明", isRequired: false),
                new("wechat_official_account_pic", "关注公众号", isRequired: false)
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
                if (childInfo.IsRequired == false)
                    continue;
                throw new UserException($"配置项:{childInfo.Key}-[{childInfo.Description}]不能为空");
            }

            childInfo.Value = value;
        }

        return dicItem;
    }
}