/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： Ilog
*创建人： Lxsh
*创建时间：2019/1/3 15:00:10
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/3 15:00:10
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Log
{
   public interface ILog
    {
        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志消息</param>
        void Write(LogLevel level, string message);
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Debug(string message);
        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Info(string message);
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Warn(string message);
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Error(string message);
        /// <summary>
        /// 严重日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Fatal(string message);
        /// <summary>是否启用日志</summary>
        Boolean Enable { get; set; }

        /// <summary>日志等级，只输出大于等于该级别的日志，默认Info</summary>
        LogLevel Level { get; set; }

    }
}