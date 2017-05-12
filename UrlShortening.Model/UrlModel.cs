using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortening.Model
{
    public class UrlModel
    {
        public UrlModel()
        {
            
        }
        public UrlModel(string shortUrl, string url)
        {
            this.ShortUrl = shortUrl;
            this.Url = url;
            this.ModifiedDate = DateTime.UtcNow;
        }

        public string ShortUrl { get; set; }

        public string Url { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
