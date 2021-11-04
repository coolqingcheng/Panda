using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.Requests;

public class DicDataRequest
{
    public DicDataGroupInfo GroupInfo { get; set; }

    public List<DicDataChildInfo> ChildInfos { get; set; }
}

public class DicDataGroupInfo
{
    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }
}

public class DicDataChildInfo
{
    [Required] public string Key { get; set; }
    [Required] public string Value { get; set; }
    [Required] public string Description { get; set; }
}