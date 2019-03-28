/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.File
*文件名： FileWatcher
*创建人： Lxsh
*创建时间：2019/1/12 12:12:18
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/12 12:12:18
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.FileOperation
{

    /// <summary>
    /// 文件监控类，用于监控指定目录下文件以及文件夹的变化
    /// </summary>
    public class FileWatcher:IDisposable
    {
        public FileSystemWatcher watcher = new FileSystemWatcher();
        public event FileSystemEventHandler fileSystemEventHandler;
        public event RenamedEventHandler renamedEventHandler;

        /// <summary>
        /// 初始化监听
        /// </summary>
        /// <param name="StrWarcherPath">需要监听的目录</param>
        /// <param name="FilterType">需要监听的文件类型(筛选器字符串)</param>
        /// <param name="IsEnableRaising">是否启用监听</param>
        /// <param name="IsInclude">是否监听子目录</param>
        public void WatcherStrat(string StrWarcherPath, string FilterType, bool IsEnableRaising, bool IsInclude)
        {
            //初始化监听
            watcher.BeginInit();
            //设置监听文件类型
            watcher.Filter = FilterType;
            //设置是否监听子目录
            watcher.IncludeSubdirectories = IsInclude;
            //设置是否启用监听?
            watcher.EnableRaisingEvents = IsEnableRaising;
            //设置需要监听的更改类型(如:文件或者文件夹的属性,文件或者文件夹的创建时间;NotifyFilters枚举的内容)
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName |
                                   NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                                   NotifyFilters.Security | NotifyFilters.Size;
            //设置监听的路径
            watcher.Path = StrWarcherPath;
            //注册创建文件或目录时的监听事件
            watcher.Created += new FileSystemEventHandler(watch_created);
            //注册当指定目录的文件或者目录发生改变的时候的监听事件
            watcher.Changed += new FileSystemEventHandler(watch_changed);
            //注册当删除目录的文件或者目录的时候的监听事件
            watcher.Deleted += new FileSystemEventHandler(watch_deleted);
            //当指定目录的文件或者目录发生重命名的时候的监听事件
            watcher.Renamed += new RenamedEventHandler(watch_renamed);
            //结束初始化
            watcher.EndInit();
        }


        /// <summary>
        /// 创建文件或者目录时的监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void watch_created(object sender, FileSystemEventArgs e)
        {
            if(fileSystemEventHandler!=null)
            fileSystemEventHandler(sender, e);
        }

        /// <summary>
        /// 当指定目录的文件或者目录发生改变的时候的监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watch_changed(object sender, FileSystemEventArgs e)
        {
            if (fileSystemEventHandler != null)
                fileSystemEventHandler(sender, e);
        }

        /// <summary>
        /// 当删除目录的文件或者目录的时候的监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watch_deleted(object sender, FileSystemEventArgs e)
        {
            if (fileSystemEventHandler != null)
                fileSystemEventHandler(sender, e);
        }

        /// <summary>
        /// 当指定目录的文件或者目录发生重命名的时候的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watch_renamed(object sender, RenamedEventArgs e)
        {
            if (renamedEventHandler != null)
                renamedEventHandler(sender, e);
        }

        /// <summary>
        /// 启动或者停止监听
        /// </summary>
        /// <param name="IsEnableRaising">True:启用监听,False:关闭监听</param>
        public void WatchStartOrSopt(bool IsEnableRaising)
        {
            watcher.EnableRaisingEvents = IsEnableRaising;
        }

        public void Dispose()
        {
            WatchStartOrSopt(false);
            watcher.Dispose();
        }
    }
}