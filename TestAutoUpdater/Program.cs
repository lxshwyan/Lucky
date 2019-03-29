using AutoUpdateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestAutoUpdater
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += delegate
           {
               cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + "异常退出");
           };
            try
            {
                InterfaceAutoUpdateMng autoupdate = null;
                bool bl;
                string strPath = Application.StartupPath + "\\AutoUpdate.exe";
                if (System.IO.File.Exists(strPath))
                {
                    autoupdate = System.Reflection.Assembly.LoadFrom(strPath).CreateInstance("AutoUpdate.AUpdateMng") as InterfaceAutoUpdateMng;
                }
                else
                {
                    autoupdate = null;
                }

                if (autoupdate == null)
                    bl = false;
                else
                    bl = autoupdate.IsUpdate();
                if (bl)
                {
                    System.Diagnostics.Process.Start(strPath);
                }
                else
                {
                    bool bCreatedNew;

                    System.Threading.Mutex m = new System.Threading.Mutex(false, "TestAutoUpdater", out bCreatedNew);

                    if (bCreatedNew)
                        Application.Run(new Form1());
                    else
                    {
                        MessageBox.Show("平台已开启!");
                    }
                }
            }
            catch (Exception ex)
            {

               cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + ex.Message);
            }
            
        }
    }
}
