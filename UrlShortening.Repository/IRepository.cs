using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortening.Model;

namespace UrlShortening.Repository
{
    public interface IRepository
    {
        Task<UrlModel> Add(UrlModel model);

        Task<UrlModel> Get(string shortUrl);
    }
}
