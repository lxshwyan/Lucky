using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoUpdateInterface
{
    #region Log Level Enum
    public enum LogLevel
    {
        Debug = 0,
        Warning = 1,
        Error = 2,
        Info = 3
    };
    #endregion // Log Level Enum
    class cGlobe_LogManager
    {
        #region Private Instance Fields

        private string logFileName = string.Empty;
        private string logPath = "log";
        private string logFileExtName = "log";
        private bool writeLogTime = true;
        private bool logFileNameEndWithDate = false;
        private Encoding logFileEncoding = Encoding.UTF8;
        private object obj = new object();

        #endregion // Private Instance Fields

        #region Public Instance Constructors

        public cGlobe_LogManager()
        {
            this.LogPath = "log";
            this.LogFileExtName = "log";
            this.WriteLogTime = true;
            this.logFileNameEndWithDate = false;
            this.logFileEncoding = Encoding.UTF8;
        }

        public cGlobe_LogManager(string logPath, string logFileExtName, bool writeLogTime)
        {
            this.LogPath = logPath;
            this.LogFileExtName = logFileExtName;
            this.WriteLogTime = writeLogTime;
            this.logFileNameEndWithDate = true;
            this.logFileEncoding = Encoding.UTF8;
        }

        #endregion // Public Instance Constructors

        #region Public Instance Properties

        public string LogPath
        {
            get
            {
                if (this.logPath == null || this.logPath == string.Empty)
                {
                    this.logPath = Application.StartupPath + @"\log\";
                }
                return this.logPath;
            }
            set
            {
                this.logPath = value;
                if (this.logPath == null || this.logPath == string.Empty)
                {
                    this.logPath = Application.StartupPath + @"\log\";
                }
                else
                {
                    try
                    {
                        if (this.logPath.IndexOf(Path.VolumeSeparatorChar) >= 0)
                        { /* 绝对路径 */}
                        else
                        {
                            // 相对路径
                            this.logPath = Application.StartupPath + @"\log\";
                        }
                        if (!Directory.Exists(this.logPath))
                            Directory.CreateDirectory(this.logPath);
                    }
                    catch
                    {
                        this.logPath = Application.StartupPath + @"\log\";
                    }
                    if (!this.logPath.EndsWith(@"\"))
                        this.logPath += @"\";
                }
            }
        }

        public string LogFileExtName
        {
            get { return this.logFileExtName; }
            set { this.logFileExtName = value; }
        }

        /// <summary>
        /// 是否在每个Log行前面添加当前时间
        /// </summary>
        public bool WriteLogTime
        {
            get { return this.writeLogTime; }
            set { this.writeLogTime = value; }
        }

        /// <summary>
        /// 日志文件名是否带日期
        /// </summary>
        public bool LogFileNameEndWithDate
        {
            get { return logFileNameEndWithDate; }
            set { logFileNameEndWithDate = value; }
        }

        /// <summary>
        /// 日志文件的字符编码
        /// </summary>
        public Encoding LogFileEncoding
        {
            get { return logFileEncoding; }
            set { logFileEncoding = value; }
        }

        #endregion // Public Instance Properties

        #region  Public Instance Methods

        public void WriteLog(LogLevel type, string name, string msg)
        {
            lock (obj)
            {
                try
                {
                    string dateString = DateTime.Now.ToString("yyyy-MM-dd");//string.Empty;
                    if (this.logFileNameEndWithDate || name.Length == 0)
                    {
                        dateString = DateTime.Now.ToString("yyyyMMdd");
                    }
                    logFileName = string.Format("{0}{1}{2}.{3}",
                                                this.LogPath,
                                                name,
                                                dateString,
                                                this.logFileExtName);
                    using (StreamWriter sw = new StreamWriter(logFileName, true, logFileEncoding))
                    {
                        string level = null;
                        switch (type)
                        {
                            case LogLevel.Debug:
                                level = "Debug";
                                break;
                            case LogLevel.Warning:
                                level = "Warning";
                                break;
                            case LogLevel.Error:
                                level = "Error";
                                break;
                            case LogLevel.Info:
                                level = "Info";
                                break;
                            default:
                                level = "Debug";
                                break;
                        }

                        if (writeLogTime)
                        {
                            sw.WriteLine("[{0} {1}]: {2}", level, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
                        }
                        else
                        {
                            sw.WriteLine("[{0}]: {2}", level, msg);
                        }
                        sw.Close();
                    }
                }
                catch
                {
                }
            }
        }

        #endregion // Public Instance Methods
    }
}
