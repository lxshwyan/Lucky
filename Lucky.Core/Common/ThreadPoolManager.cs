/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Common
*文件名： ThreadPoolManager
*创建人： Lxsh
*创建时间：2019/1/11 11:14:20
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 11:14:20
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lucky.Core.Log;

namespace Lucky.Core.Common
{
   public class ThreadPoolManager
    {
        /// <summary>
        /// 将在线程池上运行的指定工作排队
        /// </summary>
        /// <param name="action"></param>
        public static void Run(Action action)
        {
            ThreadPool.QueueUserWorkItem(q =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    LoggerManager.Instance.Info(LoggerManager.Instance.GetMethodInfo() + ex.Message);
                    throw;
                }
            });
        }
        /// <summary>
        /// 将在线程池上运行的指定工作排队
        /// </summary>
        /// <param name="callBack"></param>
        public static void Run(WaitCallback callBack)
        {
            ThreadPool.QueueUserWorkItem(u =>
            {
                try
                {
                    callBack(null);
                }
                catch (Exception ex)
                {
                    LoggerManager.Instance.Info(LoggerManager.Instance.GetMethodInfo() + ex.Message);
                }
            });

        }
    }
}