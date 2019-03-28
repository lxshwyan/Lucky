/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.CachingQueue.Queue
*文件名： MemoryQueue
*创建人： Lxsh
*创建时间：2019/1/10 9:32:12
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/10 9:32:12
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.CachingQueue.Queue
{
    /// <summary>
    ///  内存队列，键值可为空
    /// </summary>
    public class MemoryQueue : IQueue
    {
        #region 属性
        private static readonly Queue<byte[]> _queue = new Queue<byte[]>();
      
        #endregion

        public int Count
        {
            get {return _queue.Count;}
        }
         /// <summary>
         /// 内存队列，键值可为空
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
        public byte[] Pop(string key)
        {
         
            if (this.Count > 0)
                return _queue.Dequeue();
            return null;
        }
        /// <summary>
        /// 内存队列，键值可为空
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        public void Push(string key, byte[] msg)
        {
            _queue.Enqueue(msg);
        }
    }
}