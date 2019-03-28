/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.MEF
*文件名： BasePlugin
*创建人： Lxsh
*创建时间：2019/1/11 17:11:45
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 17:11:45
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.MEF
{
    public abstract class AsbBasePlugin : IPlugin
    {
      
        public PluginDescriptionAttribute PluginDescripor
        {

            get { return this.GetType().GetCustomAttributes(true).OfType<PluginDescriptionAttribute>().FirstOrDefault(); }
        }

        public virtual void  Install()
        {
           
        }

        public virtual void Uninstall()
        {
          
        }

      
    }
}