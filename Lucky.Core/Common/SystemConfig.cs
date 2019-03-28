/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Common
*文件名： SystemConfig
*创建人： Lxsh
*创建时间：2019/1/11 9:11:40
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 9:11:40
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Common
{
   public class SystemConfig
    {
        /// <summary>
        /// 获取系统下面appconfig中的AppSettings参数
        /// </summary>
        /// <param name="strKey">键值</param>
        /// <param name="strDefault">当键值对应vulue为空时，默认返回的值</param>
        /// <returns></returns>
        public static string GetSystemConfig(string strKey, string strDefault)
        {

            if (System.Configuration.ConfigurationManager.AppSettings[strKey] == null)
            {
                return strDefault;
            }
            else
            {
                return System.Configuration.ConfigurationManager.AppSettings[strKey].ToString();
            }
        }
    }
}