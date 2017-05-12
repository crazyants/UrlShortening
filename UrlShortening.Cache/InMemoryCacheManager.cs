using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UrlShortening.Contract;

namespace UrlShortening.Cache
{
    public class InMemoryCacheManager:ICacheManager
    {
        public T GetCached<T>(string cacheKey) where T : class
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                return HttpContext.Current.Cache[cacheKey] as T;
            }
            return null;
        }

        public bool TryAddToCache<T>(string cacheKey, T value, int timeout = 0)
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                httpContext.Cache.Insert(cacheKey, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings.Get("CacheTimeout"))));

            }
            return false;
        }
    }
}
