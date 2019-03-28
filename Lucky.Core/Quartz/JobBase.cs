/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Quartz
*文件名： JobBase
*创建人： Lxsh
*创建时间：2019/1/11 10:41:40
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 10:41:40
*修改人：Lxsh
*描述：
************************************************************************/
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Lucky.Core.Log;

namespace Lucky.Core.Quartz
{
    /// <summary>
    /// [DisallowConcurrentExecution]  //一个一个执行  （每次任务执行完成才能执行下一个任务）
    /// [PersistJobDataAfterExecution]  //共享  context
    /// </summary>
    public abstract  class JobBase: IJob, IDisposable
    {
        #region IJob 成员
        public void Dispose()
        {
            Console.WriteLine("当前任务执行成功已释放");
        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString() + "{0}这个Job开始执行", context.JobDetail.Key.Name);
                ExcuteJob(context);
                Console.WriteLine(DateTime.Now.ToString() + "{0}这个Job执行完成", context.JobDetail.Key.Name);

            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Info(LoggerManager.Instance.GetMethodInfo() + ex.Message);
                throw;
            }
        }

        #endregion
        /// <summary>
        /// Job具体类去实现自己的逻辑
        /// </summary>
        protected abstract void ExcuteJob(IJobExecutionContext context);
    }
}