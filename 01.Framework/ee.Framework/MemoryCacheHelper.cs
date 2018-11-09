using System;
using System.Runtime.Caching;

namespace ee.Framework
{
    /// <summary>
    /// MemoryCache
    /// </summary>
    public static class MemoryCacheHelper
    {
        private static readonly Object _locker = new object();

        public static T CacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null, bool immediatelyUpdate = false)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null) throw new ArgumentNullException("cachePopulate");
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");
            if (immediatelyUpdate && MemoryCache.Default[key] != null)
            {
                MemoryCache.Default.Remove(key);
            }
            if (MemoryCache.Default[key] == null)
            {
                lock (_locker)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
    }
}
