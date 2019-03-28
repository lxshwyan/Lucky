/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： LogLevel
*创建人： Lxsh
*创建时间：2019/1/3 14:54:02
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/3 14:54:02
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Log
{

    public enum LogLevel
    {
        /// <summary>所有消息</summary>
        On = 0,
        /// <summary>调试消息</summary>
        Debug = 1,
        /// <summary>普通消息</summary>
        Info = 2,
        /// <summary>警告消息</summary>
        Warn = 3,
        /// <summary>错误信息</summary>
        Error = 4,
        /// <summary>严重消息</summary>
        Fatal = 5,
        /// <summary>关闭消息</summary>
        Off = 99,
    }
    public enum ErrirHandle
    {    
        ///<summary>抛出异常/// </summary>
        Throw,
        ///<summary>内部消化异常，继续执行下一步代码/// </summary>
        Contine
    }
    /// <summary>
    ///  日志类型
    /// </summary>
    [Flags]
    public enum LogType
    {    [Description("控制台日志")]
        ///<summary>控制台日志/// </summary>
        ConsoleLog = 1,
        [Description("log4net日志")]
        ///<summary>log4net日志/// </summary>
        Log4netHelper = 2,
        [Description("本地文件日志")]
        ///<summary>本地文件日志/// </summary>
        TextFileLog = 4,
        [Description("sql数据库日志")]
        ///<summary>sql数据库日志/// </summary>
        SqlLog = 8,
        [Description("rabbitMQ队列日志(主题方式Exchange: Lucky.Core.Model.Base_SysLog:Lucky.Core)")]
        ///<summary>rabbitMQ队列日志(主题方式Exchange: Lucky.Core.Model.Base_SysLog:Lucky.Core)/// </summary>
        RabbitMQLog = 16,
        [Description("还未实现的日志方式)")]
        ///<summary>还未实现的日志方式/// </summary>
        Unknown = 256
    }

}