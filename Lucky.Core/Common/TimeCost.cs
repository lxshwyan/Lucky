/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： TimeCost
*创建人： Lxsh
*创建时间：2019/1/4 14:58:47
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 14:58:47
*修改人：Lxsh
*描述：
************************************************************************/

using Lucky.Core.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 统计代码的消耗时间
/// </summary>
namespace Lucky.Core.Common
{
    public class TimeCost : IDisposable
    {
        public void Dispose()
        {
            Stop();
        }
         #region 属性
          Stopwatch _sw; 
        /// <summary>名称</summary>
        public String Name { get; set; }

        /// <summary>最大时间。毫秒</summary>
        public Int32 Max { get; set; }

        /// <summary>日志输出</summary>
        public ILog Log { get; set; }
        #endregion

        #region 构造
        /// <summary>指定最大执行时间来构造一个代码时间统计</summary>
        /// <param name="name"></param>
        /// <param name="msMax"></param>
        public TimeCost(String name, Int32 msMax = 0)
        {
            Name = name;
            Max = msMax;
            Log =new ConsoleLog();

            if (msMax >= 0) Start();
        } 
        #endregion

        #region 方法
        /// <summary>开始</summary>
        public void Start()
        {
            if (_sw == null)
                _sw = Stopwatch.StartNew();
            else if (!_sw.IsRunning)
                _sw.Start();
        }

        /// <summary>停止</summary>
        public void Stop()
        {
            if (_sw != null)
            {
                _sw.Stop();

                if (Log != null&& Log.Enable)
                {
                    var ms = _sw.ElapsedMilliseconds;
                    if (ms > Max)
                    {
                        if (Max > 0)
                            Log.Warn($"{Name}执行过长警告 {ms:n0}ms > {Max:n0}ms");
                        else
                            Log.Warn($"{Name}执行 {ms:n0}ms");
                    }
                }
            }
        }
        #endregion
    }

}