/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Config
*文件名： CustomerConfig
*创建人： Lxsh
*创建时间：2019/1/29 17:30:56
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/29 17:30:56
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Config
{
   public  class CustomerConfig:ConfigurationSection
    {
        private const string RedisCacheConfigPropertyName = "redisCache";
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static CustomerConfig GetConfig()
        {
            return GetConfig("CustomerConfig");
        }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="sectionName">xml节点名称</param>
        /// <returns></returns>
        public static CustomerConfig GetConfig(string sectionName)
        {
            CustomerConfig section = (CustomerConfig)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        [ConfigurationProperty(RedisCacheConfigPropertyName)]
        public RedisCacheElement RedisCacheConfig
        {
            get { return (RedisCacheElement)base[RedisCacheConfigPropertyName]; }
            set { base[RedisCacheConfigPropertyName] = value; }
        }
    }
}