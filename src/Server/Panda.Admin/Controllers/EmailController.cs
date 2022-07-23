using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Panda.Tools.Auth.Controllers;
using Panda.Tools.Email;
using Panda.Entity.CommonModel.Setting;
using Panda.Admin.Models.Request;

namespace Panda.Admin.Controllers
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public class EmailController : AdminController
    {
        private IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task SendTest(EmailSendModel model)
        {
            await _emailSender.SendEmail(model.Email, "Test", "这是一封测试邮件", "服务器当前时间为:" + DateTime.Now);
        }
    }
}
