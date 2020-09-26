﻿using Microsoft.Extensions.DependencyInjection;
using Monica.Enums;
using Monica.Module;

namespace Monica.AspectFlare.DependencyInjection
{
    /// <summary>
    /// 动态代理模块
    /// </summary>
    public class AspectFlareModule: StartupModule
    {
        public override ModuleLevel Level => ModuleLevel.FrameWork;
        public override uint Order => 0;

        public override void ConfigueServices(IServiceCollection services)
        {
            services.UseDynamicProxyService(true);
        }
    }
}