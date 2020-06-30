using CluedIn.Crawling.Twitter.Core;

namespace CluedIn.Crawling.Twitter.Infrastructure.Factories
{
    public interface ITwitterClientFactory
    {
        TwitterClient CreateNew(TwitterCrawlJobData twitterCrawlJobData);
    }
}
