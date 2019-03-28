/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log;
*文件名： TextFileLog
*创建人： Lxsh
*创建时间：2019/1/3 17:14:03
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/3 17:14:03
*修改人：Lxsh
*描述：
************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lucky.Core.Log
{
    public class TextFileLog : LoggerBase
    {
        static readonly object objLock = new object(); 
        public string FilePath { get; set; }
        protected string _defaultLoggerName = DateTime.Now.ToString("yyyy-MM-dd");
        protected override void OnWrite(LogLevel level, string message)
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!System.IO.Directory.Exists(FilePath))
                System.IO.Directory.CreateDirectory(FilePath);
            string filePath = Path.Combine(FilePath, _defaultLoggerName);
            //写日志委托
            Action<string> write = (fileName) =>
            {
                lock (objLock)//防治多线程读写冲突
                {

                    using (System.IO.StreamWriter srFile = new System.IO.StreamWriter(fileName, true))
                    {
                        srFile.WriteLine(string.Format("[{0} {1}{2}]{3}"
                            , level.ToString()
                            , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            , (" Id:" + Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(2, '0')).PadRight(6)
                            , message));
                    }
                }
            };
            try
            {
                write(filePath + ".log");
            }
            catch (Exception ex)
            {
                write(filePath + Process.GetCurrentProcess().Id + ".log");
            }
        }
    }
}