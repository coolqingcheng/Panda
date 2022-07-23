using MailKit.Net.Smtp;
using MimeKit;
using Panda.Admin.Services.SiteOption;
using Panda.Entity.CommonModel.Setting;
using Panda.Tools.Email;

namespace Panda.Admin.Services.Email
{
    public class SiteOptionEmailSender : IEmailSender
    {
        private readonly ISiteOptionService _siteOptionService;

        public SiteOptionEmailSender(ISiteOptionService siteOptionService)
        {
            _siteOptionService = siteOptionService;
        }

        public async Task SendEmail(string address, string nickName, string title, string messageContent)
        {
            var option = await _siteOptionService.GetModel<EmailSettingModel>();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(option.NickName,
                option.UserName));
            message.To.Add(new MailboxAddress(nickName, address));
            message.Subject = title;

            message.Body = new TextPart("html")
            {
                Text = messageContent
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(option.Host,option.Port,option.UseSSL);
            // Note: only needed if the SMTP server requires authentication
            await client.AuthenticateAsync(option.UserName,option.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
