using System.Configuration;
using System;
using System.Collections.Generic;  
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Microsoft.Practices.Unity.Configuration;
/// <summary>
/// mvc使用的时候需要替换控制器  ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());//替换默认的控制器工厂
/// </summary>
namespace Lucky.WebCore.IOC
{
    /// <summary>
    /// 依赖注入工厂
    /// </summary>
    public class DIFactory
    {
        private static object _SyncHelper = new object();
        private static Dictionary<string, IUnityContainer> _UnityContainerDictionary = new Dictionary<string, IUnityContainer>();

        /// <summary>
        /// 根据containerName获取指定的container
        /// </summary>
        /// <param name="containerName">配置的containerName，默认为defaultContainer</param>
        /// <returns></returns>
        public static IUnityContainer GetContainer(string containerName = "LxshContainer")
        {
            if (!_UnityContainerDictionary.ContainsKey(containerName))
            {
                lock (_SyncHelper)
                {
                    if (!_UnityContainerDictionary.ContainsKey(containerName))
                    {
                        //配置UnityContainer
                        IUnityContainer container = new UnityContainer();
                        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                        fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");
                        Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                        UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
                        configSection.Configure(container, containerName);

                        _UnityContainerDictionary.Add(containerName, container);
                    }
                }
            }
            return _UnityContainerDictionary[containerName];
        }
    }
}
