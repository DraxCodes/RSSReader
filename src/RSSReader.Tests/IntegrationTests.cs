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
