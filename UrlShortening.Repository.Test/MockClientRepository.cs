using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using UrlShortening.Contract;
using UrlShortening.Model;

namespace UrlShortening.Repository.Test
{
    public class MockClientRepository : IClientRepository
    {
        public MockClientRepository()
        {
            this.Models = new EditableList<UrlModel>();
        }
        public List<UrlModel> Models { get; set; } 
        public async Task<UrlModel> Save(UrlModel model)
        {
            this.Models.Add(model);
            return await Task.FromResult(model);
        }

        public async Task<UrlModel> Get(string shortUrl)
        {
            return await Task.FromResult(this.Models.Single(e => e.ShortUrl == shortUrl));
        }
    }
}