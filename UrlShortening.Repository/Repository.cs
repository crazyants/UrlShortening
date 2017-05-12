using System.Threading.Tasks;
using UrlShortening.Contract;
using UrlShortening.Model;

namespace UrlShortening.Repository
{
    public class Repository:IRepository
    {
        private readonly IClientRepository _clientRepository;
        public Repository(IClientRepository clientRepository)
        {
            this._clientRepository = clientRepository;
        }

        public async Task<UrlModel> Add(UrlModel model)
        {
            var result = await this._clientRepository.Save(model);
            return result;
        }

        public async Task<UrlModel> Get(string shortUrl)
        {
            var result = await this._clientRepository.Get(shortUrl);
            return result;
        }
    }
}
