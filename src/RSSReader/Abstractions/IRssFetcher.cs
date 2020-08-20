using RSSReader.Models;
using System;
using System.Collections.Generic;

namespace RSSReader.Abstractions
{
    public interface IRssFetcher
    {
        IEnumerable<FeedDataModel> FetchAllArticles(string url);
    }
}