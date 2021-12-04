﻿using EasyCaching.Core;
using EasyCaching;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Panda.Admin.Controllers
{
    public class CacheController : AdminController
    {
        private readonly IEasyCachingProvider _cachingProvider;

        public CacheController(IEasyCachingProvider cachingProvider)
        {
            _cachingProvider = cachingProvider;
        }

        [AllowAnonymous]
        [HttpGet]
        public Dictionary<string, string> GetAllKv()
        {
            var a = CacheKeys.GetAllKv();
            return a!;
        }

        [HttpGet]
        public async Task Clear(string key)
        {
            if (key.EndsWith("_"))
            {
                await _cachingProvider.RemoveByPrefixAsync(key);
            }
            else
            {
                await _cachingProvider.RemoveAsync(key);
            }
        }
    }
}