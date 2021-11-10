using Panda.Entitys.Responses;

namespace Panda.Entity.Responses;

public class PageDto<T> : BaseResponse
{
    public List<T> Data { get; set; }

    public int Total { get; set; }
}