/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： RabbitLog
*创建人： Lxsh
*创建时间：2019/1/4 11:28:16
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 11:28:16
*修改人：Lxsh
*描述：
************************************************************************/
using Lucky.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;

namespace Lucky.Core.Log
{
    ///<summary>输出rabbitMQ队列日志(主题方式Exchange: Lucky.Core.Model.Base_SysLog:Lucky.Core)/// </summary>
    public class RabbitMQLog : LoggerBase
    {

        IBus bus;
        public RabbitMQLog(string RabbitMQConnect = "")
        {
            if (string.IsNullOrEmpty(RabbitMQConnect))
            {
               RabbitMQConnect = System.Configuration.ConfigurationManager.AppSettings["RabbitMQConnect"] != null ? System.Configuration.ConfigurationManager.AppSettings["RabbitMQConnect"].ToString() :
                  "host=127.0.0.1:5672;virtualHost=vhost_lxsh;username=lxsh;password=123456";
            }
            try
            {
                bus = RabbitHutch.CreateBus(RabbitMQConnect);
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }
        protected override void OnWrite(LogLevel level, string message)
        {
            Base_SysLog base_SysLog = new Base_SysLog()
            {
                LogContent = message,
                LogCreateTime = DateTime.Now.ToString(),
                LogOrigin = "System",
                LogType = level.ToString()

            };   
            bus.Publish(base_SysLog, "RabbitMQ.Log");
        }
    }
}