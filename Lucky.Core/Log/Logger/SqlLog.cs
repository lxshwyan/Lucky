/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Log
*文件名： SqlLog
*创建人： Lxsh
*创建时间：2019/1/4 10:42:21
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 10:42:21
*修改人：Lxsh
*描述：
************************************************************************/
using Lucky.Core.Model;
using Lucky.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Log
{
    ///<summary>sql数据库日志/// </summary>
    public class SqlLog : LoggerBase
    {
        IRepository<Base_SysLog> repository=new SqlSugarRepository<Base_SysLog>() ;
        public SqlLog(string connString="")
        {
        
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
            repository.Insert(base_SysLog);
        }
    }
}