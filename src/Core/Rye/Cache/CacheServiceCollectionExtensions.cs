﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rye.Cache;
using Rye.Module;

namespace Rye
{
    public static class CacheServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Rye框架对内存缓存的支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRyeCacheMemory(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.TryAddSingleton<ICacheService, CacheService>();
            return services;
        }

        /// <summary>
        /// 添加缓存模块
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCacheModule(this IServiceCollection services)
        {
            return services.AddModule<CacheModule>();
        }
    }
}
