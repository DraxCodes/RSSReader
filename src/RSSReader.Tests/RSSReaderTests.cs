using RSSReader.Abstractions;
using RSSReader.Exceptions;
using RSSReader.Tests.MockXml;
using System.Linq;
using Xunit;

namespace RSSReader.Tests
{
    [Trait("TestType", "Unit")]
    public class RSSReaderTests
    {
        private IRssFetcher _rssFetcher;
        private readonly IXmlReaderProvider _xmlReaderProvider;

        public RSSReaderTests()
        {
            _xmlReaderProvider = new XmlReaderProvider();
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
        public void FeedWithoutSummary_ShouldReturnEmptySummaryString()
        {
            var expected = string.Empty;
            _rssFetcher = new RssFetcher(new MockXmlReaderProviderNullSummary());

            var result = _rssFetcher.FetchAllArticles("http://mocked-rss-feed.com");
            var firstResult = result.First();

            Assert.Equal(expected, firstResult.Summary);
        }

        [Fact]
        public void FeedWithoutTitle_ShouldReturnEmptyTitleString()
        {
            var expected = string.Empty;
            _rssFetcher = new RssFetcher(new MockXmlReaderProviderNullTitle());

            var result = _rssFetcher.FetchAllArticles("http://mocked-rss-feed.com");
            var firstResult = result.First();

            Assert.Equal(expected, firstResult.Title);
        }

        [Fact]
        public void FeedWithoutUrl_SHouldReturnEmptyUrlString()
        {
            var expected = string.Empty;
            _rssFetcher = new RssFetcher(new MockXmlReaderProviderNullUrl());

            var result = _rssFetcher.FetchAllArticles("http://mocked-rss-feed.com");
            var firstResult = result.First();

            Assert.Equal(expected, firstResult.Url);
        }
    }
}
