using Panda.Entitys.Responses;

namespace Panda.Entity.Responses;

public class PageResponse<T>:BaseResponse
{
    public T Data { get; set; }

    public long Total { get; set; }
    
}