using Es.Udc.DotNet.MiniPortal.Model.Properties;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.Caching
{
    public class CachingProvider : ICachingProvider
    {
        protected MemoryCache cache = new MemoryCache("CachingProvider");

        static readonly object padlock = new object();

        public void AddItem(string key, object value)
        {
            lock (padlock)
            {
                byte cacheLimit = Settings.Default.cacheLimit;

                if (cache.Count() >= cacheLimit) // last queries
                {
                    cache.Remove(cache.FirstOrDefault().Key);
                }
                cache.Add(key, value, DateTimeOffset.MaxValue);
            }
        }

        public void RemoveItem(string key)
        {
            lock (padlock)
            {
                cache.Remove(key);
            }
        }

        public object GetItem(string key, bool remove)
        {
            lock (padlock)
            {
                var res = cache[key];

                if (res != null)
                {
                    if (remove == true)
                        cache.Remove(key);
                }
                return res;
            }
        }

        public object GetItem(string key)
        {
            lock (padlock)
            {
                var res = cache[key];
                return res;
            }
        }

        public void Clean()
        {
            cache = new MemoryCache("CachingProvider");
        }
    }
}


