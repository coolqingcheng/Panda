using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Panda.Tools.Email;

public interface IEmailSender
{
    /// <summary>
    ///     发送邮件
    /// </summary>
    /// <param name="address">地址</param>
    /// <param name="nickName">对方昵称</param>
    /// <param name="title">标题</param>
    /// <param name="messageContent">模板内容</param>
    /// <returns></returns>
    Task SendEmail(string address, string nickName, string title, string messageContent);
}

public class DefaultEmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public DefaultEmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmail(string address, string nickName, string title, string messageContent)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_configuration.GetSection("Email:user:nickname").Value,
            _configuration.GetSection("Email:user:username").Value));
        message.To.Add(new MailboxAddress(nickName, address));
        message.Subject = title;

        message.Body = new TextPart("html")
        {
            Text = messageContent
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_configuration.GetSection("Email:stmp:host").Value,
            Convert.ToInt32(_configuration.GetSection("Email:stmp:port").Value),
            Convert.ToBoolean(_configuration.GetSection("Email:stmp:usessl").Value));
        // Note: only needed if the SMTP server requires authentication
        await client.AuthenticateAsync(_configuration.GetSection("Email:user:username").Value,
            _configuration.GetSection("Email:user:pwd").Value);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}