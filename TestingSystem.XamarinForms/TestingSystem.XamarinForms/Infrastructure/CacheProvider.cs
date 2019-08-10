using Microsoft.Extensions.Caching.Memory;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestingSystem.XamarinForms.Infrastructure
{
    public class CacheProvider
    {
        //private readonly IMemoryCache cache;

        public CacheProvider()
        {
            //cache = new MemoryCache(new MemoryCacheOptions() {});
        }

        public void Set<T>(string key, T value)
        {
            Barrel.Current.Add<T>(key, value, TimeSpan.FromMinutes(30));
            //cache.Set(key, value, new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) });
        }

        public T Get<T>(string key)
        {
            if (Barrel.Current.Exists(key))
                return Barrel.Current.Get<T>(key);
            else
                return default(T);
            //if (cache.TryGetValue(key, out T value))
            //    return value;
            //else
            //    return default(T);
        }
    }
}
