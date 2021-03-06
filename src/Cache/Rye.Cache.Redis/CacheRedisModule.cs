﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Rye.Enums;
using Rye.Module;
using System;

namespace Rye.Cache.Redis
{
    /// <summary>
    /// 添加Redis缓存模块
    /// </summary>
    /// <returns></returns>
    [DependsOnModules(typeof(CacheModule))]
    public class CacheRedisModule : StartupModule
    {
        public override ModuleLevel Level => ModuleLevel.FrameWork;
        public override uint Order => 1;

        private Action<RedisCacheOptions> _action;

        public CacheRedisModule(Action<RedisCacheOptions> action = null)
        {
            _action = action;
        }

        public override void ConfigueServices(IServiceCollection services)
        {
            services.RemoveAll<IDistributedCache>();
            services.AddRyeCacheRedis(_action);
        }
    }
}
