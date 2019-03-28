using System.Linq;
using System.Web.Mvc;       
using Unity.AspNet.Mvc;

//在程序启动之前运行，需加这2句代码
//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Lucky.WebCore.IOC.UnityWebActivator), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Lucky.WebCore.IOC.UnityWebActivator), "Shutdown")]

namespace Lucky.WebCore.IOC
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}