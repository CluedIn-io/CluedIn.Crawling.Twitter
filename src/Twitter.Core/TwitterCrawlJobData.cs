using CluedIn.Core.Crawling;

namespace CluedIn.Crawling.Twitter.Core
{
    public class TwitterCrawlJobData : CrawlJobData
    {
        public string ApiKey { get; set; }
        public string Screen_name { get; set; }
    }
}
