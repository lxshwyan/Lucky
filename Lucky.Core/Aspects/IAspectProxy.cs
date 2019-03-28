using Lucky.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Aspects
{
    /// <summary>
    /// 支持AOP拦截的接口,它被认为是一种插件动态注入到系统中
    /// </summary>
    public interface IAspectProxy : IPlugins { }
}
