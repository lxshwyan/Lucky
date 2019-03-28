/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Config
*文件名： RedisCacheElement
*创建人： Lxsh
*创建时间：2019/1/29 17:31:56
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/29 17:31:56
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
    public class RedisCacheElement : ConfigurationElement
    {
        private const string EnablePropertyName = "enabled";

        private const string ConnectionStringPropery = "connectionString";

        [ConfigurationProperty(EnablePropertyName, IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)base[EnablePropertyName]; }
            set { base[EnablePropertyName] = value; }
        }

        [ConfigurationProperty(ConnectionStringPropery, IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)base[ConnectionStringPropery]; }
            set { base[ConnectionStringPropery] = value; }
        }
    }
}