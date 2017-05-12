using System;
using Moq;
using Xunit;
using UrlShortening.Model;

namespace UrlShortening.Repository.Test
{
    public class RepositoyTest
    {
        [Fact]
        public void AddTest()
        {
            var mockClient = new MockClientRepository();
            var repository = new Repository(mockClient);
            var model = repository.Add(new UrlModel("http://short.com/abcd", "http://www.google.com")).Result;
            Assert.Equal(model.ShortUrl, "http://short.com/abcd");
            Assert.Equal(model.Url, "http://www.google.com");
        }

        [Fact]
        public void GetTest()
        {
            var mockClient = new MockClientRepository();
            var repository = new Repository(mockClient);
            var model = repository.Add(new UrlModel("http://short.com/abcd", "http://www.google.com")).Result;
            var retModel = repository.Get("http://short.com/abcd").Result;
            Assert.Equal("http://short.com/abcd", retModel.ShortUrl);
            Assert.Equal("http://www.google.com", retModel.Url);
        }


    }
}
