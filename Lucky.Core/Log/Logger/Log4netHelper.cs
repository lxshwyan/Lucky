/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： Log4netHelper
*创建人： Lxsh
*创建时间：2019/1/4 10:42:21
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 10:42:21
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Lucky.Core.Log
{
    /// <summary>
    /// 从log4net输出日志信息
    /// </summary>
    public class Log4netHelper : LoggerBase
    {
       
        public Log4netHelper(string log4netFileName = "CfgFiles\\log4net.Config")
        {  
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, log4netFileName)));
            loger = log4net.LogManager.GetLogger(typeof(Log4netHelper));
            loger.Info("系统初始化Logger模块");
        }
        private log4net.ILog loger = null;
        public Log4netHelper(Type type,string log4netFileName= "CfgFiles\\log4net.Config")
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, log4netFileName)));
            loger = log4net.LogManager.GetLogger(type);
            loger.Info("系统初始化Logger模块");
        }
        protected override void OnWrite(LogLevel level, string message)
        {  
            switch (level)
            {
                case LogLevel.Debug:
                    loger.Debug(message);
                    break;
                case LogLevel.Warn:
                    loger.Warn(message);
                    break;
                case LogLevel.Error:
                    loger.Error(message);
                    break;
                case LogLevel.Info:
                    loger.Info(message);
                    break;
                case LogLevel.Fatal:
                    loger.Fatal(message);
                    break;
            }
        }
    }
}