using RSSReader.Abstractions;
using RSSReader.Exceptions;
using RSSReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSSReader
{
    public class RssFetcher : IRssFetcher
    {
        private readonly IXmlReaderProvider _xmlReader;

        public RssFetcher(IXmlReaderProvider xmlReader)
        {
            _xmlReader = xmlReader;
        }

        public IEnumerable<FeedDataModel> FetchAllArticles(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new InvalidUrlException("Supplied URL is invalid.");

            var uri = new Uri(url);

            return GetFeedItems(uri);
        }

        private IEnumerable<FeedDataModel> GetFeedItems(Uri uri)
        {
            var feedData = FetchFeedData(uri);
            var feedItems = new List<FeedDataModel>();
            if (feedData.Items.Any())
            {
                foreach (var item in feedData.Items)
                {
                    var feedModel = BuildFeedModel(item);
                    feedItems.Add(feedModel);
                }
            }

            return feedItems;
        }

        private SyndicationFeed FetchFeedData(Uri uri)
        {
            try
            {
                var reader = _xmlReader.Create(uri.AbsoluteUri);
                var feed = SyndicationFeed.Load(reader);
                reader.Close();
                return feed;
            }
            catch (XmlException)
            {
                throw new InvalidRssFeedException("Provided RSS Feed URL is invalid.");
            }
        }

        private FeedDataModel BuildFeedModel(SyndicationItem item)
            => new FeedDataModel
            {
                Title = item.Title?.Text ?? string.Empty,
                Summary = item.Summary?.Text ?? string.Empty,
                PublishDate = item.PublishDate,

                Url = item.Links.Any()
                    ? item.Links.First().Uri.AbsoluteUri
                    : string.Empty
            };
    }
}