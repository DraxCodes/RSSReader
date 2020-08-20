using RSSReader.Abstractions;
using RSSReader.Exceptions;
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
    public class RSSReaderTests
    {
        private IRssFetcher _rssFetcher;
        private readonly IXmlReaderProvider _xmlReaderProvider;

        public RSSReaderTests()
        {
            _xmlReaderProvider = new XmlReaderProvider();
        }

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
        public void InvalidUri_SouldThrowInvalidUriException()
        {
            const string invalidUrl = "ClearlyInvalidUrl.com";
            _rssFetcher = new RssFetcher(_xmlReaderProvider);

            Assert.Throws<InvalidUrlException>(() => _rssFetcher.FetchAllArticles(invalidUrl));
        }

        [Fact]
        public void InvalidRssFeedUrl_ShouldThrowInvalidRssFeedException()
        {
            const string validUrlButNotRssFeed = "https://duckduckgo.com/";
            _rssFetcher = new RssFetcher(_xmlReaderProvider);


            Assert.Throws<InvalidRssFeedException>(() => _rssFetcher.FetchAllArticles(validUrlButNotRssFeed));
        }

        [Fact]
        public void ValidFeedUrl_ShouldReturnItems()
        {
            const string validRssFeedUrl = "https://www.theverge.com/rss/index.xml";
            _rssFetcher = new RssFetcher(_xmlReaderProvider);

            var result = _rssFetcher.FetchAllArticles(validRssFeedUrl);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void FeedWithoutSummary_ShouldReturnEmptySummaryString()
        {
            var expected = string.Empty;
            _rssFetcher = new RssFetcher(new MockXmlReaderProvderNullSummary());

            var result = _rssFetcher.FetchAllArticles("http://fake.com");
            var firstResult = result.First();

            Assert.Equal(expected, firstResult.Summary);
        }

        [Fact]
        public void FeedWithoutTitle_ShouldReturnEmptyTitleString()
        {
            var expected = string.Empty;
            _rssFetcher = new RssFetcher(new MockXmlReaderProvderNullTitle());

            var result = _rssFetcher.FetchAllArticles("http://fake.com");
            var firstResult = result.First();

            Assert.Equal(expected, firstResult.Title);
        }

        [Fact]
        public void FeedWithoutUrl_SHouldReturnEmptyUrlString()
        {
            var expected = string.Empty;
            _rssFetcher = new RssFetcher(new MockXmlReaderProvderNullUrl());

            var result = _rssFetcher.FetchAllArticles("http://fake.com");
            var firstResult = result.First();

            Assert.Equal(expected, firstResult.Url);
        }
    }
}
