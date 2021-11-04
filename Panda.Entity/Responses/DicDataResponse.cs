using Panda.Entity.Requests;

namespace Panda.Entity.Responses;

public class DicDataResponse
{
    public DicDataGroupInfo GroupInfo { get; set; }

    public IEnumerable<DicDataChildInfo> ChildInfo { get; set; }
}