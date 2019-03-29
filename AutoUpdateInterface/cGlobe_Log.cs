using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace AutoUpdateInterface
{
    public static class cGlobe_Log
    {
        #region Private Fields

        private static cGlobe_LogManager m_logManager = null;
        private static string m_userName = null;
        public static string loginUser
        {
            get
            {
                return m_userName;
            }
            set
            {
                m_userName = value;
            }
        }

        #endregion // Private Fields

        #region Constructors

        static cGlobe_Log()
        {
            if (m_logManager == null)
                m_logManager = new cGlobe_LogManager();
        }

        #endregion // Constructors

        #region  Public Methods
        public static string GetMethodInfo()
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
            //str += mb.DeclaringType.Namespace + ".";
            ////取得父方法类名
            //str += mb.DeclaringType.Name + ".";
            //取得父方法类全名
            str += mb.DeclaringType.FullName + ".";
            //取得父方法名
            str += mb.Name + ":";
            return str;
        }

        public static void Debug(string format, params object[] args)
        {
            Debug(String.Format(format, args));
        }

        public static void Debug(string msg)
        {
            string name = null;

            if (Thread.CurrentThread.Name == null)
            {
                name = Process.GetCurrentProcess().ProcessName;
            }
            else
            {
                name = Thread.CurrentThread.Name;
            }

            m_logManager.WriteLog(LogLevel.Debug, name, msg);
        }

        public static void Warning(string format, params object[] args)
        {
            Warning(String.Format(format, args));
        }

        public static void Warning(string msg)
        {
            string name = null;

            if (Thread.CurrentThread.Name == null)
            {
                name = Process.GetCurrentProcess().ProcessName;
            }
            else
            {
                name = Thread.CurrentThread.Name;
            }

            m_logManager.WriteLog(LogLevel.Warning, name, msg);
        }

        public static void Error(string format, params object[] args)
        {
            Error(String.Format(format, args));
        }

        public static void Error(string msg)
        {
            string name = null;

            if (Thread.CurrentThread.Name == null)
            {
                name = Process.GetCurrentProcess().ProcessName;
            }
            else
            {
                name = Thread.CurrentThread.Name;
            }

            m_logManager.WriteLog(LogLevel.Error, name, msg);
        }

        public static void Info(string format, params object[] args)
        {
            Info(String.Format(format, args));
        }

        public static void Info(string msg)
        {
            string name = null;

            if (Thread.CurrentThread.Name == null)
            {
                name = Process.GetCurrentProcess().ProcessName;
            }
            else
            {
                name = Thread.CurrentThread.Name;
            }

            m_logManager.WriteLog(LogLevel.Info, name, msg);
        }

        #endregion // Public Methods
    }
}
