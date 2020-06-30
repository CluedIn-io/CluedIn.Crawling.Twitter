using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Twitter.Infrastructure.Factories;
using Moq;

namespace CluedIn.Provider.Twitter.Unit.Test.TwitterProvider
{
    public abstract class TwitterProviderTest
    {
        protected readonly ProviderBase Sut;

        protected Mock<ITwitterClientFactory> NameClientFactory;
        protected Mock<IWindsorContainer> Container;

        protected TwitterProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            NameClientFactory = new Mock<ITwitterClientFactory>();
            var applicationContext = new ApplicationContext(Container.Object);
            Sut = new Twitter.TwitterProvider(applicationContext, NameClientFactory.Object);
        }
    }
}
