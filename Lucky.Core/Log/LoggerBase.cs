/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： LoggerBase
*创建人： Lxsh
*创建时间：2019/1/3 15:08:24
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/3 15:08:24
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Log
{
    public abstract class LoggerBase : ILog
    {

        #region 属性
        /// <summary>是否启用日志。默认true</summary>
        public virtual Boolean Enable { get; set; } = true;

        private LogLevel? _Level;
        /// <summary>默认为Info日志信息</summary>
        public virtual LogLevel Level
        {
            get
            {
                if (_Level != null) return _Level.Value;

                return LogLevel.Info;
            }
            set { _Level = value; }
        }
        #endregion

        #region 接口方法   
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public virtual void Debug(string message)
        {
            Write(LogLevel.Debug, message);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public virtual void Error(string message)
        {
            Write(LogLevel.Error, message);
        }
        /// <summary>
        /// 严重错误日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public virtual void Fatal(string message)
        {
            Write(LogLevel.Fatal, message);
        }
        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public virtual void Info(string message)
        {
            Write(LogLevel.Info, message);
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public virtual void Warn(string message)
        {
            Write(LogLevel.Warn, message);  
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="message">日志信息</param>
        public virtual void Write(LogLevel level, string message)
        {
            if (Enable && level >= Level)
            {
                OnWrite(level, message);
            }
        }
        #endregion

        #region 子类继承实现方法
        protected abstract void OnWrite(LogLevel level, string message);
        #endregion

        #region 辅助方法
        /// <summary>格式化参数，特殊处理异常和时间</summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual String Format(String format, Object[] args)
        {
            //处理时间的格式化
            if (args != null && args.Length > 0)
            {
                // 特殊处理异常
                if (args.Length == 1 && args[0] is Exception ex && (string.IsNullOrEmpty(format) || format == "{0}"))
                    throw new Exception("format格式化参数不能为空");

                for (var i = 0; i < args.Length; i++)
                {
                    if (args[i] != null && args[i].GetType() == typeof(DateTime))
                    {
                        // 根据时间值的精确度选择不同的格式化输出
                        var dt = (DateTime)args[i];
                        if (dt.Millisecond > 0)
                            args[i] = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        else if (dt.Hour > 0 || dt.Minute > 0 || dt.Second > 0)
                            args[i] = dt.ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            args[i] = dt.ToString("yyyy-MM-dd");
                    }
                }
            }
            if (args == null || args.Length < 1) return format;       

            return String.Format(format, args);
        }      
        #endregion

      

    }
}