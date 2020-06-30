using CluedIn.Crawling.Twitter.Core;

namespace CluedIn.Crawling.Twitter
{
    public class TwitterCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<TwitterCrawlJobData>
    {
        public TwitterCrawlerJobProcessor(TwitterCrawlerComponent component) : base(component)
        {
        }
    }
}
