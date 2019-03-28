using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Lucky.Core.Config;
using Unity;
using Lucky.Core.TypeFinder;
using Lucky.Core.IOC;
using Lucky.WebCore.Infrastucture;

namespace Lucky.WebCore.IOC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        public static bool Init = false;
        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            if (!Init)
            {
                Init = true;
                RegisterTypes(ServiceContainer.Current);  
            }
           
            return ServiceContainer.Current;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance(container);

            ITypeFinder typeFinder = new WebTypeFinder();

            //注入自定义配置文件
             var config = ConfigurationManager.GetSection("CustomerConfig") as CustomerConfig; 
             container.RegisterInstance(config);

            //找到继承IDependencyRegister类型的所有实例
            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();

            foreach (Type registerType in registerTypes)
            {
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterTypes(container);
            }
        }
    }
}
