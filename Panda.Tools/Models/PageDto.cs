namespace Panda.Entity.Responses;

public class PageDto<T> : BaseResponse
{
    public List<T> Data { get; set; }

    public int Total { get; set; }
}

public class BaseResponse
{
    /// <summary>
    ///     消息
    /// </summary>
    public string Message { get; set; }
}