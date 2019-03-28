/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.CachingQueue
*文件名： IQueue
*创建人： Lxsh
*创建时间：2019/1/10 9:28:38
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/10 9:28:38
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.CachingQueue
{
    public interface IQueue
    {
        /// <summary>
        ///  添加到队列
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="msg">值</param>
        void Push(string key, byte[] msg);
        /// <summary>
        /// 从队列获取值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        byte[] Pop(string key);
        /// <summary>
        /// 得到队列的项总数
        /// </summary>
        int Count { get; }
    }
}