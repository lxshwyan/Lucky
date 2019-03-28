using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucky.Core.Common;
using Lucky.Core.Extention;
using StackExchange.Redis;

namespace Lucky.Core.RedisClient
{
    /// <summary>
    /// StackExchange.Redis管理者     
    /// 注意：这个客户端没有连接池的概念，需要注意生产redis的数量
    /// 由于支持的集群环境，所以不能做成单例
    /// </summary>
    public class RedisManager
    {
      
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _locker = new object();
        /// <summary>
        /// StackExchange.Redis对象
        /// </summary>
        private static ConnectionMultiplexer _redis;     
        /// <summary>
        /// 得到StackExchange.Redis单例对象
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_redis == null || !_redis.IsConnected)
                {
                    lock (_locker)
                    {
                        if (_redis == null || !_redis.IsConnected)
                        {
                            string config = SystemConfig.GetSystemConfig("Redisconfig", "localhost:6379");
                             _redis = GetManager(config);
                            return _redis;
                        }
                    }
                }

                return _redis;
            }
        }    
        /// <summary>
        /// 构建链接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static ConnectionMultiplexer GetManager(string config)
        {                                                      
            return ConnectionMultiplexer.Connect(config);
        }


      


    }

}
