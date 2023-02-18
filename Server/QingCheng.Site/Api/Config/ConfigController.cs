using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QingCheng.Site.Services.Config;
using QingCheng.Site.Services.Config.Services;
using QingCheng.Tools.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Api.Config
{
    public class ConfigController : BaseAdminController
    {
        private readonly ConfigService _config;

        public ConfigController(ConfigService config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task Test([FromServices] IConfiguration configuration)
        {
            await _config.UpdateConfig(new Dictionary<string, string> { { "test-1", "mb" } });
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SetSite(SiteConfig config)
        {
            var kv = config.GetKv();
            await _config.UpdateConfig(kv);
        }

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<SiteConfig> GetSiteConfig()
        {
            return await _config.Get<SiteConfig>();
        }
    }
}