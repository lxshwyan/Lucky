/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Model
*文件名： Base_SysLog
*创建人： Lxsh
*创建时间：2019/1/4 10:47:35
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 10:47:35
*修改人：Lxsh
*描述：
************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Model
{
   
    public class Base_SysLog
    {
        /// <summary>
        /// 编号
        /// </summary>   
        public int ID { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }
        /// <summary>
        /// 日志来源
        /// </summary>
        public string LogOrigin { get; set; }
        /// <summary>
        /// 日志产生时间
        /// </summary>
        public string LogCreateTime { get; set; }
    }
}