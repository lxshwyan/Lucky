/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： CompositeLog
*创建人： Lxsh
*创建时间：2019/1/3 16:30:30
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/3 16:30:30
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
    /// <summary>复合日志提供者，多种方式输出</summary>
    public class CompositeLog : LoggerBase
    {
        private Boolean _Enable;
        public override Boolean Enable
        {
            get
            {
               return _Enable;  
            }
            set
            {
                _Enable = value;
                foreach (LoggerBase item in Logs)
                {
                    item.Enable = Enable;
                }
            }
        }
        private LogLevel? _Level;
        public override LogLevel Level
        {
            get
            {
                if (_Level != null) return _Level.Value;

                return LogLevel.Info;
            }
            set
            {
                _Level = value;
                foreach (LoggerBase item in Logs)
                {
                    item.Level = Level;
                }
            }
        }
        /// <summary>
        ///组合日志
        /// </summary>
        private List<LoggerBase> Logs { get; set; }
        /// <summary>实例化</summary>
        public CompositeLog() { Logs = new List<LoggerBase>(); }

        /// <summary>实例化</summary>
        /// <param name="log"></param>
        public CompositeLog(LoggerBase log) { Logs.Add(log); Level = log.Level; }
        /// <summary>添加一个日志提供者</summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public CompositeLog Add(LoggerBase log) { Logs.Add(log); return this; }
        /// <summary>删除日志提供者</summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public CompositeLog Remove(LoggerBase log) { if (Logs.Contains(log)) Logs.Remove(log); return this; }
        public CompositeLog RemoveAll() { Logs=new List<LoggerBase> (); return this; }
        protected override void OnWrite(LogLevel level, string message)
        { 
            if (Logs != null)
            {
                foreach (var item in Logs)
                {
                    item.Write(level, message);
                }
            }
        }  
        /// <summary>从复合日志提供者中提取指定类型的日志提供者</summary>
        /// <typeparam name="TLog"></typeparam>
        /// <returns></returns>
        public TLog Get<TLog>() where TLog : class
        {
            foreach (var item in Logs)
            {
                if (item != null)
                {
                    if (item is TLog) return item as TLog;

                    // 递归获取内层日志
                    if (item is CompositeLog cmp)
                    {
                        var log = cmp.Get<TLog>();
                        if (log != null) return log;
                    }
                }
            }

            return null;
        }
        /// <summary>已重载。</summary>
        /// <returns></returns>
        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.Append(GetType().Name);

            foreach (var item in Logs)
            {
                sb.Append(" ");
                sb.Append(item + "");
            }

            return sb.ToString();
        }
    }

}