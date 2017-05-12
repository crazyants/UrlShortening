using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using UrlShortening.AwsClient;
using UrlShortening.Cache;
using UrlShortening.Contract;
using UrlShortening.Model;
using UrlShortening.Repository;
using UrlShorteningService.Extensions;
using UrlShorteningService.Models;
using UrlShorteningService.Utility;

namespace UrlShorteningService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ShorteningController:ApiController
    {
        private readonly IRepository _repository;
        private readonly ICacheManager _cacheManager;
        private readonly UtilityManager _utilityManager;

        public ShorteningController(IRepository repository, ICacheManager cacheManager, UtilityManager utilityManager)
        {
            this._repository = repository;
            this._cacheManager = cacheManager;
            this._utilityManager = utilityManager;
        }

        [HttpPost]
        [Route("st")]
        public async Task<IHttpActionResult> Post(UrlDto url)
        {
            try
            {
                Regex urlchk = new Regex(@"((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,15})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (urlchk.Match(url.Url).Length <= 0)
                {
                    return this.BadRequest("Invalid URL");
                }

                if (string.IsNullOrEmpty(url.Url))
                {
                    return NotFound();
                }

                var shortUrl = this._utilityManager.ConvertToShortUrl(url.Url);
                var model = await this._repository.Add(new UrlModel(shortUrl, url.Url));
                var cached = this._cacheManager.GetCached<UrlModel>(model.ShortUrl);
                if (cached == null)
                {
                    this._cacheManager.TryAddToCache<UrlModel>(model.ShortUrl, model);
                }
                return this.Ok(model.ToWellFormattedObject());
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("st/{key}")]
        public async Task<HttpResponseMessage> Get(string key)
        {
            var cachedObject = this._cacheManager.GetCached<UrlModel>(key);

            if (cachedObject == null)
            {
                cachedObject = await this._repository.Get(key);
                if (cachedObject == null)
                {
                    return null;
                }
                this._cacheManager.TryAddToCache(cachedObject.ShortUrl, cachedObject);
            }

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(cachedObject.Url);
            return response;
        }
    }
}