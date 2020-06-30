using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.Twitter;
using CluedIn.Crawling.Twitter.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Twitter.Unit.Test
{
    public class TwitterCrawlerBehaviour
    {
        private readonly ICrawlerDataGenerator _sut;

        public TwitterCrawlerBehaviour()
        {
            var nameClientFactory = new Mock<ITwitterClientFactory>();

            _sut = new TwitterCrawler(nameClientFactory.Object);
        }

        [Fact]
        public void GetDataReturnsData()
        {
            var jobData = new CrawlJobData();

            _sut.GetData(jobData)
                .ShouldNotBeNull();
        }
    }
}
