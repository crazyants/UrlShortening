using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linq2DynamoDb.DataContext;
using UrlShortening.Contract;
using UrlShortening.Model;

namespace UrlShortening.AwsClient
{
    public class AwsRepository: IClientRepository
    {
        private readonly AwsClient _client;
        public AwsRepository()
        {
            this._client = new AwsClient();
            this._client.Connect();
        }
        public async Task<UrlModel> Save(UrlModel model)
        {
            var context = this._client.Datacontext<DataContext>();
            var existingModel = context.GetTable<UrlModel>().SingleOrDefault(e => e.Url == model.Url);
            if (existingModel != null)
            {
                return await Task.FromResult(existingModel);
            }

            context.GetTable<UrlModel>().InsertOnSubmit(model);
            await context.SubmitChangesAsync();
            return await Task.FromResult(model);
        }

        public async Task<UrlModel> Get(string shortUrl)
        {
            var context = this._client.Datacontext<DataContext>();
            var existingModel = context.GetTable<UrlModel>().SingleOrDefault(e => e.ShortUrl == shortUrl);            
            return await Task.FromResult(existingModel);
        }


    }
}
