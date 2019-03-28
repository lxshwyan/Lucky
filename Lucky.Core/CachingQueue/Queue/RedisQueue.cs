/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.CachingQueue.Queue
*文件名： RedisQueue
*创建人： Lxsh
*创建时间：2019/1/10 9:39:48
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/10 9:39:48
*修改人：Lxsh
*描述：
************************************************************************/
using Lucky.Core.Extention;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.CachingQueue.Queue
{
   public class RedisQueue : IQueue
    {
        #region 属性
     
        private const string CACHE_QUEUE_KEY = "Lucky.Core.CachingQueue.Queue";
        private ConnectionMultiplexer _redisConnection { get; }
        private int _databaseIndex { get; }
        #endregion
        #region 构造方法
        /// <summary>
        /// 默认构造函数
        /// 注：使用默认配置，即localhost:6379,无密码
        /// </summary>
        public RedisQueue()
        {
            _databaseIndex = 0;
            string config = System.Configuration.ConfigurationManager.AppSettings["Redisconfig"];
            if (config.IsNullOrEmpty())
            {
                throw new ArgumentNullException("请去检查config文件是否有Redisconfig节点");
            }
            _redisConnection = ConnectionMultiplexer.Connect(config);

        }
        #endregion
        public int Count
        {
            get
            {
                return (int)_cacheManager.ListLength(CACHE_QUEUE_KEY);
            }
        }
        private  IDatabase _cacheManager { get => _redisConnection.GetDatabase(_databaseIndex); }
        public byte[] Pop(string key)
        {
            return _cacheManager.ListLeftPop(CACHE_QUEUE_KEY + "_" + key); 
        }

        public void Push(string key, byte[] msg)
        {
            _cacheManager.ListLeftPush(CACHE_QUEUE_KEY + "_" + key, msg);
        }
    }
}