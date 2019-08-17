using Microsoft.Extensions.Caching.Memory;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.XamarinForms.Infrastructure
{
    public class CacheProvider
    {
        public void Set<T>(string key, T value)
        {
            Barrel.Current.Add<T>(key, value, TimeSpan.FromMinutes(5));
        }

        public T Get<T>(string key)
        {
            if (Barrel.Current.Exists(key))
                return Barrel.Current.Get<T>(key);
            else
                return default(T);
        }

        public void Remove(string key)
        {
            if (Barrel.Current.Exists(key))
                Barrel.Current.Empty(key);
        }

        public Task SetAsync<T>(string key, T value)
        {
            return Task.Run(() => Set<T>(key, value));
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.Run(() => Get<T>(key));
        }

        public Task RemoveAsync(string key)
        {
            return Task.Run(() => Remove(key));
        }
    }
}
