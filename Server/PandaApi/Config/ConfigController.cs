using Microsoft.AspNetCore.Mvc;
using Panda.Services.Config;
using Panda.Services.Config.Services;

namespace PandaApi.Config
{
    public class ConfigController : BaseAdminController
    {
        private readonly ConfigService _config;

        public ConfigController(ConfigService config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task Test()
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