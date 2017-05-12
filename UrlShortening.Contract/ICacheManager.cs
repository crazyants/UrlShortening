using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortening.Contract
{
    public interface ICacheManager
    {
        T GetCached<T>(string cacheKey) where T : class;

        bool TryAddToCache<T>(string cacheKey, T value, int timeout = 0);
    }
}
