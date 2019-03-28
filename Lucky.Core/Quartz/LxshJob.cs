/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Quartz
*文件名： LxshJob
*创建人： Lxsh
*创建时间：2019/1/11 11:04:58
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 11:04:58
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace Lucky.Core.Quartz
{
    public class LxshJob : JobBase
    {
        static int index = 1;
        protected override void ExcuteJob(IJobExecutionContext context)
        {

            var info = string.Format("{4} index={0},current={1}, scheuler={2},nexttime={3}",
                index++, DateTime.Now,
                context.ScheduledFireTimeUtc?.LocalDateTime,
                context.NextFireTimeUtc?.LocalDateTime,
                context.JobDetail.JobDataMap["key"]);
            Console.WriteLine(info);
        }
    }
}