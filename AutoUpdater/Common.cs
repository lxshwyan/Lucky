/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：AutoUpdate
*文件名： Common
*创建人： Lxsh
*创建时间：2019/3/29 12:16:19
*描述
*=======================================================================
*修改标记
*修改时间：2019/3/29 12:16:19
*修改人：Lxsh
*描述：
************************************************************************/
using AutoUpdateInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public class Common
    {
        /// <summary>
        /// 比对版本号大小
        /// </summary>
        /// <param name="AsVer1"></param>
        /// <param name="AsVer2"></param>
        /// <returns></returns>
        public static int CompareVersion(string newVer, string oldVer)
        {
            try
            {
                Version v1 = new Version(newVer);
                Version v2 = new Version(oldVer);
                if (v1 > v2)
                {
                    return 1;
                }
                else if (v1 <v2)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + ex.Message);
                
            }
             return 0;

        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="objPath"></param>
        public static void CopyFile(string sourcePath, string objPath)
        {
            try
            {
                if (!Directory.Exists(objPath))
                {
                    Directory.CreateDirectory(objPath);
                }
                string[] files = Directory.GetFiles(sourcePath);
                for (int i = 0; i < files.Length; i++)
                {
                    string[] childfile = files[i].Split('\\');
                    File.Copy(files[i], objPath + @"\" + childfile[childfile.Length - 1], true);
                }
                string[] dirs = Directory.GetDirectories(sourcePath);
                for (int i = 0; i < dirs.Length; i++)
                {
                    string[] childdir = dirs[i].Split('\\');
                    CopyFile(dirs[i], objPath + @"\" + childdir[childdir.Length - 1]);
                }
            }
            catch (Exception ex)
            {
                cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + ex.Message);
            }
        }
    }

}