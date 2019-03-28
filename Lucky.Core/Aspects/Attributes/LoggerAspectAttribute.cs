
using Lucky.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Aspects
{

    /// <summary>
    /// 方法执行前拦截，并记录日志
    /// </summary>
    public class LoggerAspectAttribute : BeforeAspectAttribute
    {
        public override object FuncInvoke(InvokeContext context, MethodInfo methodInfo)
        {
            Console.WriteLine(context.Method.MethodName + " run start!");
            LoggerManager.Instance.Info(context.Method.MethodName + "这个方法开始执行");
            return null;
        }
    }

    /// <summary>
    /// 方法执行完成后拦截，并记录日志
    /// </summary>
    public class LoggerEndAspectAttribute : AfterAspectAttribute
    {
        public override object FuncInvoke(InvokeContext context, MethodInfo methodInfo)
        {
            Console.WriteLine(context.Method.MethodName + " run end!");
            LoggerManager.Instance.Info(context.Method.MethodName + "这个方法开始执行");
            return null;
        }
    }
}
