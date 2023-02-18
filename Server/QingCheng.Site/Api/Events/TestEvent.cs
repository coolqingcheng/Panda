using System.Globalization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace QingCheng.Site.Api.Events;

public class TestEvent : IRequestHandler<TestModel, string>
{
    private readonly ILogger _logger;

    public TestEvent(ILogger<TestEvent> logger)
    {
        _logger = logger;
    }

    public Task<string> Handle(TestModel request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("log:" + request.Time);
        return Task.FromResult("当前时间:" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
    }
}

public class TestModel : IRequest<string>
{
    public DateTime Time { get; set; }
}