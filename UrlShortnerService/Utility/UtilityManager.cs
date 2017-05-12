using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using UrlShortening.Contract;
using UrlShortening.Model;

namespace UrlShorteningService.Utility
{
    public class UtilityManager
    {
        public String ConvertToShortUrl(string actualUrl)
        {
            if (!string.IsNullOrWhiteSpace(actualUrl))
            {
                actualUrl = actualUrl.Trim().ToLower();
            }
            
            try
            {
                var url = new Uri(actualUrl);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid actualUrl");
            }

            String newKey = null;
            while (string.IsNullOrEmpty(newKey))
            {
                newKey = Guid.NewGuid().ToString("N").Substring(0, int.Parse(ConfigurationManager.AppSettings.Get("KeyLength"))).ToLower();
            }
            return newKey;
        }        
    }
}