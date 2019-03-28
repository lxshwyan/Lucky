/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Cache
*文件名： NullCache
*创建人： Lxsh
*创建时间：2019/1/29 16:27:56
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/29 16:27:56
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Cache
{
    public class NullCache : ICache
    {
        public bool ContainsKey(string key)
        {
            return false;
        }

        public object GetCache(string key)
        {
            return null;
        }

        public T GetCache<T>(string key) where T : class
        {
            return default(T);
        }

        public void RemoveCache(string key)
        {
           
        }

        public void SetCache(string key, object value)
        {
           
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
          
        }

        public void SetKeyExpire(string key, TimeSpan expire)
        {
            
        }

        public void RemoveCache()
        {
            
        }
    }
}