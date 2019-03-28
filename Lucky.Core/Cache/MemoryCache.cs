/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Cache
*文件名： MemoryCache
*创建人： Lxsh
*创建时间：2019/1/29 16:29:50
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/29 16:29:50
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Lucky.Core.Cache
{
    public class MemoryCache : ICache
    {
        public bool ContainsKey(string key)
        {
            return System.Runtime.Caching.MemoryCache.Default.Contains(key);
        }

        public object GetCache(string key)
        {
            return System.Runtime.Caching.MemoryCache.Default.Get(key);
        }

        public T GetCache<T>(string key) where T : class
        {
            return (T)System.Runtime.Caching.MemoryCache.Default.Get(key);
        }

        public void RemoveCache(string key)
        {
            System.Runtime.Caching.MemoryCache.Default.Remove(key);
        }

        public void SetCache(string key, object value)
        {
            System.Runtime.Caching.MemoryCache.Default.Add(key, value, new CacheItemPolicy() { Priority = CacheItemPriority.NotRemovable});
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            System.Runtime.Caching.MemoryCache.Default.Add(key, value, new CacheItemPolicy { SlidingExpiration = timeout });
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            System.Runtime.Caching.MemoryCache.Default.Add(key, value, new CacheItemPolicy { SlidingExpiration = timeout });
        }

        public void SetKeyExpire(string key, TimeSpan expire)
        {
            System.Runtime.Caching.MemoryCache.Default.Set(key, GetCache(key), new CacheItemPolicy { SlidingExpiration = expire });
        }

        public void RemoveCache()
        {
            foreach (var item in System.Runtime.Caching.MemoryCache.Default)
            {
                this.RemoveCache(item.Key);
            }
        }
    }
}