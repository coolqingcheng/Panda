using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.Requests;

public class DicDataRequest
{
    public DicDataRequest()
    {
    }

    public DicDataRequest(DicDataGroupInfo groupInfo, List<DicDataChildInfo> childInfos)
    {
        GroupInfo = groupInfo;
        ChildInfos = childInfos;
    }

    public DicDataGroupInfo GroupInfo { get; set; }

    public List<DicDataChildInfo> ChildInfos { get; set; }
}

public class DicUpdateRequest
{
    [Required] public string GroupKey { get; set; }

    [Required] public Dictionary<string, string> List { get; set; }
}

public class DicDataGroupInfo
{
    public DicDataGroupInfo()
    {
    }

    public DicDataGroupInfo(string name, string description)
    {
        Name = name;
        Description = description;
    }

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }
}

public class DicDataChildInfo
{
    public DicDataChildInfo()
    {
    }

    public DicDataChildInfo(string key, string description, bool isSecrecy = false, bool isRequired = true)
    {
        Key = key;
        Description = description;
        IsSecrecy = isSecrecy;
        IsRequired = isRequired;
    }

    public bool IsRequired { get; set; }

    [Required] public string Key { get; set; }
    [Required] public string? Value { get; set; }
    [Required] public string Description { get; set; }

    /// <summary>
    ///     是否保密
    /// </summary>
    public bool IsSecrecy { get; set; }
}