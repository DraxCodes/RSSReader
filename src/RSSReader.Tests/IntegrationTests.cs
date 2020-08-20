using RSSReader.Abstractions;
using RSSReader.Models;
using RSSReader.Tests.MockXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Xunit;

namespace RSSReader.Tests
{
    [Trait("TestType", "Integration")]
    public class IntegrationTests
    {

        [Theory]
        [InlineData("https://www.theverge.com/rss/index.xml")]
        [InlineData("http://feeds.feedburner.com/TechCrunch/")]
        public void ProofOfConcept(string url)
        {
            var uri = new Uri(url);
            var reader = XmlReader.Create(uri.AbsoluteUri);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();

            var feedData = new List<FeedDataModel>();

            foreach (var item in feed.Items)
            {
                feedData.Add(new FeedDataModel
                {
                    Title = item.Title.Text,
                    Summary = item.Summary?.Text ?? string.Empty,
                    Url = item.Links.First().Uri.AbsoluteUri
                });
            }

            Assert.NotEmpty(feedData);
        }

        [Fact]
        public void ValidFeedXml_NoValuesShouldBeNull()
        {
            var _rssFetcher = new RssFetcher(new MockXmlReaderProviderFullXml());

            var result = _rssFetcher.FetchAllArticles("http://totally-real-rss-feed.com");

            Assert.NotEmpty(result);
            AssertAllModelValuesNotNull(result);
        }

        private void AssertAllModelValuesNotNull(IEnumerable<FeedDataModel> models)
        {
            foreach (var model in models)
            {
                Assert.NotNull(model.Title);
                Assert.NotNull(model.Summary);
                Assert.NotNull(model.Url);
            }
        }
    }
}
