/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：AutoUpdate
*文件名： UpdateInfo
*创建人： Lxsh
*创建时间：2019/3/28 15:51:20
*描述
*=======================================================================
*修改标记
*修改时间：2019/3/28 15:51:20
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate
{
    /// <summary>
    /// 用于封装更新信息的实体类
    /// </summary>
    public class UpdateInfo
    {
        /// <summary>
        ///   更新版本
        /// </summary>
        public string UpdateVersion { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string UpdateFileUrl { get; set; }  
        /// <summary>
        /// 更新文件集合
        /// </summary>
        public List<UpdateFile> FileList{ get; set; }
        
        /// <summary>
        ///应用程序启动入口
        /// </summary>
        public string ApplicationName { get; set; }

    }
    /// <summary>
    /// 用于封装的待更新文件实体类
    /// </summary>
    public class UpdateFile
    {     
          public string  FileVer { get; set; }
          public string  FileName { get; set; }
          public string  FileContentLength { get; set; }
          public string  FileDownProcess { get; set; }
    }
}