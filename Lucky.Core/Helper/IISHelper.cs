/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Helper
*文件名： IISHelper
*创建人： Lxsh
*创建时间：2019/3/28 11:29:40
*描述
*=======================================================================
*修改标记
*修改时间：2019/3/28 11:29:40
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Web.Administration;
using System.Linq;
using System.Threading;
using System.Collections;

namespace Lucky.Core.Helper
{
  public  class IISHelper
    {
        /// <summary>
        /// 根据名字重启站点.(没重启线程池)
        /// </summary>
        /// <param name="sitename"></param>
      public  static void RestartWEbSite(string sitename)
        {
            try
            {
                var server = new ServerManager();
                var site = server.Sites.FirstOrDefault(s => s.Name == sitename);
                if (site != null)
                {
                    site.Stop();
                    if (site.State == ObjectState.Stopped)
                    {
                    }
                    else
                    {
                        Console.WriteLine("Could not stop website!");
                        throw new InvalidOperationException("Could not stop website!");
                    }
                    site.Start();
                }
                else
                {
                    Console.WriteLine("Could not find website!");

                    throw new InvalidOperationException("Could not find website!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 重启完之后.要再检测下.是否开启了
        /// </summary>
        /// <param name="sitename"></param>
      public  static void FixWebsite(string sitename)
        {
            try
            {
                var server = new ServerManager();
                var site = server.Sites.FirstOrDefault(s => s.Name == sitename);
                if (site != null)
                {
                    if (site.State != ObjectState.Started)
                    {
                        Thread.Sleep(500);

                        //防止状态为正在开启
                        if (site.State != ObjectState.Started)
                        {
                            site.Start();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// 线程池名字
        /// </summary>
        /// <param name="name"></param>
       public static void RestartIISPool(string name)
        {
            string[] cmds = { "c:", @"cd %windir%\system32\inetsrv", string.Format("appcmd stop apppool /apppool.name:{0}", name), string.Format("appcmd start apppool /apppool.name:{0}", name) };
            Cmd(cmds);
            CloseProcess("cmd.exe");
        }

        /// <summary>
        /// 运行CMD命令
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <returns></returns>
        public static string Cmd(string[] cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.AutoFlush = true;
            for (int i = 0; i < cmd.Length; i++)
            {
                p.StandardInput.WriteLine(cmd[i]);
            }
            p.StandardInput.WriteLine("exit");

            string strRst = p.StandardOutput.ReadToEnd();

            //Debug.Print(strRst);

            p.WaitForExit();
            p.Close();
            return strRst;
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="ProcName">进程名称</param>
        /// <returns></returns>
        public static bool CloseProcess(string ProcName)
        {
            bool result = false;
            var procList = new ArrayList();
            foreach (Process thisProc in Process.GetProcesses())
            {
                var tempName = thisProc.ToString();
                int begpos = tempName.IndexOf("(") + 1;
                int endpos = tempName.IndexOf(")");
                tempName = tempName.Substring(begpos, endpos - begpos);
                procList.Add(tempName);
                if (tempName == ProcName)
                {
                    if (!thisProc.CloseMainWindow())
                        thisProc.Kill(); // 当发送关闭窗口命令无效时强行结束进程
                    result = true;
                }
            }
            return result;
        }


    }
}