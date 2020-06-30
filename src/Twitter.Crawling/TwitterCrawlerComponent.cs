using CluedIn.Core;
using CluedIn.Crawling.Twitter.Core;

using ComponentHost;

namespace CluedIn.Crawling.Twitter
{
    [Component(TwitterConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class TwitterCrawlerComponent : CrawlerComponentBase
    {
        public TwitterCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

