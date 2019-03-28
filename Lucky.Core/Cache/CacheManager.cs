using System;
using System.Configuration;

namespace Lucky.Core.Cache
{
    /// <summary>
    /// 缓存管理类
    /// </summary>
    public class CacheManager : ICache
    {
        private static Object _objectInstance = new object();
        public static CacheManager _instance;
        public static ICache _Cache { get;private set; } //策略模式
        #region 构造方法
        public CacheManager()
        {
            string CacheType = ConfigurationManager.AppSettings["CacheType"];
            if (string.IsNullOrEmpty(CacheType))
            {
                CacheType = "RedisCache";
                //throw new Exception("未配置缓存类型！");
            }
            switch (CacheType)
            {
                case "RedisCache": _Cache = new RedisCache(); break;
                case "NullCache": _Cache = new NullCache(); break;
                case "MemoryCache": _Cache = new MemoryCache(); break;
                default: throw new Exception("请指定缓存类型！");
            }
        }
        #endregion

        #region 单例
        public static CacheManager Instance
        {
            get
            {
                if (_instance == null)        //双层判断优化性能+线程安全
                {
                    lock (_objectInstance)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheManager();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion   

        #region 缓存方法
        public void SetCache(string key, object value)
        {
            _Cache.SetCache(key, value);
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            _Cache.SetCache(key, value, timeout);
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            _Cache.SetCache(key, value, timeout, expireType);
        }

        public void SetKeyExpire(string key, TimeSpan expire)
        {
            _Cache.SetKeyExpire(key, expire);
        }

        public object GetCache(string key)
        {
            return _Cache.GetCache(key);
        }

        public T GetCache<T>(string key) where T : class
        {
            return _Cache.GetCache<T>(key);
        }

        public bool ContainsKey(string key)
        {
            return _Cache.ContainsKey(key);
        }

        public void RemoveCache(string key)
        {
            _Cache.RemoveCache(key);
        }

        public void RemoveCache()
        {
            _Cache.RemoveCache();
        }
        #endregion
    }
    /// <summary>
    /// 默认缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 系统缓存
        /// </summary>
        SystemCache,

        /// <summary>
        /// Redis缓存
        /// </summary>
        RedisCache
    }
}
