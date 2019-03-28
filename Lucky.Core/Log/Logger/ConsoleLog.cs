/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： ConsoleLog
*创建人： Lxsh
*创建时间：2019/1/3 15:51:04
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/3 15:51:04
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Log
{
    /// <summary>
    /// 输出控制台日志信息
    /// </summary>
    public class ConsoleLog : LoggerBase
    {
        /// <summary>是否使用多种颜色，默认使用</summary>
        public Boolean UseColor { get; set; } = true;
        protected override void OnWrite(LogLevel level, string message)
        {

            var e = WriteLogEventArgs.Current.Set(level).Set(message, null);

            if (!UseColor)
            {
                ConsoleWriteLog(e);
                return;
            }
            lock (this)
            {  
                var cc = Console.ForegroundColor;
                switch (level)
                {
                    case LogLevel.Warn:
                        cc = ConsoleColor.Yellow;
                        break;
                    case LogLevel.Error:
                    case LogLevel.Fatal:
                        cc = ConsoleColor.Red;
                        break;
                    default:
                        cc = GetColor(e.ThreadID);
                        break;
                }    
                Console.ForegroundColor = cc;
                ConsoleWriteLog(e);

            }
        }
        private void ConsoleWriteLog(WriteLogEventArgs e)
        {
            var msg = e.ToString();
            Console.WriteLine(msg);
        }
        static ConcurrentDictionary<Int32, ConsoleColor> dic = new ConcurrentDictionary<Int32, ConsoleColor>();
        static ConsoleColor[] colors = new ConsoleColor[] {
            ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Yellow,
            ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow };
        private ConsoleColor GetColor(Int32 threadid)
        {
            if (threadid == 1) return ConsoleColor.Gray;

            return dic.GetOrAdd(threadid, k => colors[dic.Count % colors.Length]);
        }    
        /// <summary>已重载。</summary>
        /// <returns></returns>
        public override String ToString()
        {
            return String.Format("{0} UseColor={1}", GetType().Name, UseColor);
        }
    }
}