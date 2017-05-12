using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortening.Contract
{
    public interface IClient
    {
        T Datacontext<T>() where T : class;

        void Connect();
    }
}
