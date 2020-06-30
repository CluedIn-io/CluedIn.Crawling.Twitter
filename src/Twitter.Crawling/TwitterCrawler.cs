using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.Twitter.Core;
using CluedIn.Crawling.Twitter.Infrastructure.Factories;

namespace CluedIn.Crawling.Twitter
{
    public class TwitterCrawler : ICrawlerDataGenerator
    {
        private readonly ITwitterClientFactory clientFactory;
        public TwitterCrawler(ITwitterClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is TwitterCrawlJobData twittercrawlJobData))
            {
                yield break;
            }

            var client = clientFactory.CreateNew(twittercrawlJobData);

            //retrieve data from provider and yield objects
            if (!string.IsNullOrWhiteSpace(twittercrawlJobData.ApiKey))
            {
                yield return client.GetUser(twittercrawlJobData.ApiKey);
                foreach (var item in client.GetTweets(twittercrawlJobData.ApiKey))
                {
                    item.createdByScreenName = twittercrawlJobData.Screen_name;
                    yield return item;
                }
                foreach (var item in client.GetFollowers(twittercrawlJobData.ApiKey))
                {
                    item.following_user = twittercrawlJobData.Screen_name;
                    yield return item;
                }
            }
            
        }       
    }
}
