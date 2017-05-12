using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Linq2DynamoDb.DataContext;
using UrlShortening.Contract;
using System.Configuration;

namespace UrlShortening.AwsClient
{
    public class AwsClient:IClient
    {
        private AmazonDynamoDBClient _client = null;

        public T Datacontext<T>() where T:class
        {
            if (this._client == null)
            {
                this.Connect();
            }
            var context = new DataContext(this._client) as T;

            return context;
        }

        public void Connect()
        {
            if (this._client == null)
            {
                AmazonDynamoDBConfig dbConfig = new AmazonDynamoDBConfig
                {
                    RegionEndpoint = RegionEndpoint.APSoutheast1
                };
                try
                {
                    _client = new AmazonDynamoDBClient(new BasicAWSCredentials(ConfigurationManager.AppSettings.Get("AccessKey"), ConfigurationManager.AppSettings.Get("Secret")), dbConfig);                    
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
