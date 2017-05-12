using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UrlShortening.Model;

namespace UrlShorteningService.Extensions
{
    public static class Extension
    {
        public static UrlModel ToWellFormattedObject(this UrlModel model)
        {
            var serviceBase = ConfigurationManager.AppSettings.Get("ServiceBase");
            model.ShortUrl = $"{serviceBase}/st/{model.ShortUrl}";
            return model;
        }
    }
}