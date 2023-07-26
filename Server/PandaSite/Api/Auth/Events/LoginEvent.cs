using MediatR;
using Microsoft.Net.Http.Headers;
using Panda.Models.Data.Entitys;

namespace PandaSite.Api.Auth.Events
{
    public class LoginEvent : INotification
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Accounts Account { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否登录成功
        /// </summary>
        public bool isLogin { get; set; }
    }

    public class LoginNotificationHandler : INotificationHandler<LoginEvent>
    {
        readonly IHttpContextAccessor _http;
        readonly DbContext _context;
        public LoginNotificationHandler(IHttpContextAccessor http, DbContext context)
        {
            _http = http;
            _context = context;
        }

        public async Task Handle(LoginEvent notification, CancellationToken cancellationToken)
        {
            var ip = _http.HttpContext?.GetClientIP();
            var ua = _http.HttpContext?.Request.Headers[HeaderNames.UserAgent].ToString();
            if (notification.Account != null)
            {
                await _context.Set<AccountLoginRecord>().AddAsync(new AccountLoginRecord()
                {
                    Account = notification.Account,
                    UA = ua ?? "",
                    Ip = ip ?? "",
                    Message = notification.Message,
                    IsLogin = notification.isLogin
                });
            }
            await _context.SaveChangesAsync();

        }
    }
}
