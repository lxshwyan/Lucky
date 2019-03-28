/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core
*文件名： LoggerFactory
*创建人： Lxsh
*创建时间：2019/1/4 10:08:06
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 10:08:06
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Log
{
    /// <summary>
    /// 享元工厂设计模式（确保每种日志类型只实例化一次）
    /// 日志工厂，一共可以生成种单一的日志类型：控制台，本地文件，log4net，sql数据库，rabbitMQ队列
    /// 以上五种可以任意组合生成日志组件
    /// </summary>
    public class LoggerManager : ILog
    {
        #region 属性
        private static readonly object objLock = new object();
        private static LoggerManager _instance;
        private ILog _iLog;
        private bool _Enable = true;
         /// <summary>
         /// 是否开启日志（默认开启）
         /// </summary>
        public bool Enable{ get { return _Enable; } set { _Enable = value; _iLog.Enable = _Enable; } }
      
        private LogLevel _Level = LogLevel.Info;
        /// <summary>
        /// 日志输出等级（默认Info）
        /// </summary>
        public LogLevel Level { get { return _Level; } set { _Level = value; _iLog.Level = _Level; } }
        #endregion

        #region 构造方法
        public LoggerManager()
        {
            if (ConfigurationManager.AppSettings["LogType"] == null)
            {
                throw new ArgumentNullException("请去检查config文件是否有LogType节点");
            }
            if (ConfigurationManager.AppSettings["logLevel"] == null)
            {
                throw new ArgumentNullException("请去检查config文件是否有logLevel节点");
            }
            string LogType = ConfigurationManager.AppSettings["LogType"].ToString();
            string logLevel = ConfigurationManager.AppSettings["logLevel"].ToString();
            LogType Type = GetLogType(LogType);
            LogLevel level = GetLogLevel(logLevel);
            CreateLogger(Type, level);
        }
        #endregion

        #region 单例模式
        /// <summary>
        /// 单例模式的日志工厂对象
        /// </summary>
        public static LoggerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoggerManager();
                        }
                    }
                }
                return _instance;
            }
        }

      
        #endregion

        #region 工厂方法
        /// <summary>
        /// 从日志工厂获取日志类型
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="strlevel">日志输出等级</param>
        private void CreateLogger(LogType logType, LogLevel level = LogLevel.Info)
        {      
            switch (logType)
            {
                case LogType.ConsoleLog:
                    _iLog = new ConsoleLog();
                    break;
                case LogType.TextFileLog:
                    _iLog = new TextFileLog();
                    break;
                case LogType.Log4netHelper:
                    _iLog = new Log4netHelper();
                    break;
                case LogType.SqlLog:
                    _iLog = new SqlLog();
                    break;
                case LogType.RabbitMQLog:
                    _iLog = new RabbitMQLog();
                    break;
                case LogType.Unknown:
                    throw new NotImplementedException("暂时没有实现该日志类型，目前已实现：ConsoleLog,TextFileLog,Log4netHelper,SqlLog,RabbitMQLog");

                default:
                    _iLog = new CompositeLog();
                    if ((logType & LogType.ConsoleLog) > 0) { (_iLog as CompositeLog).Add(new ConsoleLog()); }
                    if ((logType & LogType.TextFileLog) > 0) { (_iLog as CompositeLog).Add(new TextFileLog()); }
                    if ((logType & LogType.Log4netHelper) > 0) { (_iLog as CompositeLog).Add(new Log4netHelper()); }
                    if ((logType & LogType.SqlLog) > 0) { (_iLog as CompositeLog).Add(new SqlLog()); }
                    if ((logType & LogType.RabbitMQLog) > 0) { (_iLog as CompositeLog).Add(new RabbitMQLog()); }
                    break;
            }
            _Level = level;
           _iLog.Level = _Level;
           _iLog.Enable = _Enable;
        }
        /// <summary>
        /// 从日志工厂获取日志类型
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="strlevel">日志输出等级</param>  
        private void CreateLogger(string logType, string strlevel = "Info")
        {
            LogType Type = GetLogType(logType);
            LogLevel level = GetLogLevel(strlevel);
             CreateLogger(Type, level);
        }
        /// <summary>
        /// 从日志工厂获取日志类型(默认从web配置文件取LogType，logLevel)
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 转换日志类型
        /// </summary>
        /// <param name="dbtype">日志类型字符串</param>
        /// <returns>日志类型</returns>
        private static LogType GetLogType(string logType)
        {
            if (string.IsNullOrEmpty(logType))
                throw new ArgumentNullException("获取日志类型居然不传日志类型，你在逗我么？");
            LogType returnValue = LogType.Unknown;
            foreach (LogType ItemType in Enum.GetValues(typeof(LogType)))
            {
                if (logType.ToUpper().Contains(ItemType.ToString().ToUpper()))
                {
                    if (returnValue == LogType.Unknown)
                    {
                        returnValue = ItemType;
                    }
                    else
                    {
                        returnValue = returnValue | ItemType;
                    }
                }
            }
            return returnValue;
        }
        private static LogLevel GetLogLevel(string level)
        {
            if (string.IsNullOrEmpty(level))
                throw new ArgumentNullException("获取日志输出等级居然不传日志输出等级，你在逗我么？");
            LogLevel returnValue = LogLevel.Info;
            foreach (LogLevel ItemType in Enum.GetValues(typeof(LogLevel)))
            {
                if (ItemType.ToString().Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = ItemType;
                    break;
                }
            }
            return returnValue;
        }
        #endregion   

        #region Loger方法
        public void Write(LogLevel level, string message)
        {
            this._iLog.Write(level, message);
        }

        public void Debug(string message)
        {
            this._iLog.Debug(message);
        }

        public void Info(string message)
        {
            this._iLog.Info(message);
        }

        public void Warn(string message)
        {
            this._iLog.Warn(message);
        }

        public void Error(string message)
        {
            this._iLog.Error(message);
        }

        public void Fatal(string message)
        {
            this._iLog.Fatal(message);
        }
        #endregion

        #region 封装try 、catch 、finally 操作 方法
        /// <summary>
        /// 封装try 、catch 、finally 操作
        /// </summary>                   
        /// <param name="logInfo">日志信息</param>   
        /// <param name="finallyHandle">最终处理代码</param>
        /// <param name="tryHandle">运行代码</param>
        /// <param name="catchHandle">异常处理代码</param>
        ///   /// <param name="errHandle">异常处理方式</param>
        public  void Logger(string logInfo, Action tryHandle, Action<Exception> catchHandle = null, Action finallyHandle = null, ErrirHandle errHandle = ErrirHandle.Contine)
        {
            try
            {
                this.Debug(logInfo);
                tryHandle.Invoke();
            }
            catch (Exception ex)
            {
                this.Error(logInfo + ":" + ex.Message);
                if (catchHandle != null)
                {
                    catchHandle.Invoke(ex);
                }
                if (errHandle == ErrirHandle.Throw)
                {
                    throw (ex);
                }
            }
            finally
            {
                if (finallyHandle != null)
                {
                    finallyHandle.Invoke();
                }
            }

        }
        #endregion

        #region 封装记录代码段执行时间
        /// <summary>
        /// 封装记录代码段执行时间
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="action"></param>
        public  void LoggerTimer(string funcName, Action action)
        {
            StringBuilder str = new StringBuilder();
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            str.Append(funcName);
            action();
            str.Append("LoggerTimer:代码段运行时间(" + sw.ElapsedMilliseconds + "毫秒)");
            Debug(str.ToString());
            sw.Stop();
        }
        #endregion

        #region 获取方法运行的命名空间、类名、方法名
        /// <summary>
        /// 获取方法运行的命名空间、类名、方法名
        /// </summary>
        /// <returns></returns>
        public string GetMethodInfo()
        {
            string str = "";

            ////取得当前方法命名空间
            //str += "命名空间名:" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "\n";
            ////取得当前方法类全名
            //str += "类名:" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "\n";
            ////取得当前方法名
            //str += "方法名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n";
            //str += "\n";

            StackTrace ss = new StackTrace(true);

            MethodBase mb = ss.GetFrame(1).GetMethod();

            ////取得父方法命名空间
            str += mb.DeclaringType.Namespace + ".";
            ////取得父方法类名
            //str += mb.DeclaringType.Name + ".";
            //取得父方法类全名
            str += mb.DeclaringType.FullName + ".";

            //取得父方法名
            str += mb.Name + ":";

            return str;
        }
        #endregion
    }
}