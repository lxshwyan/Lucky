/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.MEF
*文件名： PluginDescriptionAttribute
*创建人： Lxsh
*创建时间：2019/1/11 17:16:51
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 17:16:51
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
    //标记只能标记类上 只能标记一次  不能继承
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited = false)]
   public class PluginDescriptionAttribute: Attribute
    {
        public PluginDescriptionAttribute(string pluginName, string pluginVesion=null, string autor=null)
        {
            this.PluginName = pluginName;
            this.PluginVesion = pluginVesion;
            this.Autor = autor;
        }

        public string PluginName { get; set; }
        public string Autor { get; set; }
        public string PluginVesion { get; set; }
    }
}