using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using System.Configuration;
using System.Linq;
using CluedIn.Core.Configuration;
using CluedIn.Crawling.Twitter.Core;
using CluedIn.Crawling.Twitter.Infrastructure.Factories;
using CluedIn.Providers.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;

namespace CluedIn.Provider.Twitter
{
    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
    public class TwitterProvider : ProviderBase, IExtendedProviderMetadata
    {
        private readonly ITwitterClientFactory _twitterClientFactory;

        private static string consumerKey = ConfigurationManager.AppSettings.GetValue<string>("Providers.TwitterConsumerKey", null);
        private static string consumerSecret = ConfigurationManager.AppSettings.GetValue<string>("Providers.TwitterConsumerSecret", null);

        public TwitterProvider([NotNull] ApplicationContext appContext, ITwitterClientFactory twitterClientFactory)
            : base(appContext, TwitterConstants.CreateProviderMetadata())
        {
            _twitterClientFactory = twitterClientFactory;
        }

        public override async Task<CrawlJobData> GetCrawlJobData(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var twitterCrawlJobData = new TwitterCrawlJobData();

            var authUrl = "https://api.twitter.com/oauth2/token";
            var authHeaderFormat = "Basic {0}";
            var authHeader = string.Format(authHeaderFormat, Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(consumerKey) + ":" + Uri.EscapeDataString(consumerSecret))));
            var postBody = "grant_type=client_credentials";
            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(authUrl);
            authRequest.Headers.Add("Authorization", authHeader);
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            using (Stream stream = authRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }
            WebResponse authResponse = authRequest.GetResponse();

            TwitAuthenticateResponse twitAuthResponse;

            using (authResponse)
            {
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    var objectText = reader.ReadToEnd();
                    twitAuthResponse = JsonConvert.DeserializeObject<TwitAuthenticateResponse>(objectText);
                    twitterCrawlJobData.ApiKey = twitAuthResponse.access_token;
                }
            }

            if (configuration.ContainsKey(TwitterConstants.KeyName.ApiKey))
            { twitterCrawlJobData.ApiKey = twitAuthResponse.access_token; }
            if (configuration.ContainsKey(TwitterConstants.KeyName.Screen_name))
            {
                twitterCrawlJobData.Screen_name = configuration[TwitterConstants.KeyName.Screen_name].ToString();
            }

            return await Task.FromResult(twitterCrawlJobData);
        }

        public override Task<bool> TestAuthentication(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            throw new NotImplementedException();
        }

        public override Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context, IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            [NotNull] CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            var dictionary = new Dictionary<string, object>();

            if (jobData is TwitterCrawlJobData twitterCrawlJobData)
            {
                //TODO add the transformations from specific CrawlJobData object to dictionary
                // add tests to GetHelperConfigurationBehaviour.cs
                dictionary.Add(TwitterConstants.KeyName.ApiKey, twitterCrawlJobData.ApiKey);
            }

            return await Task.FromResult(dictionary);
        }

        public override Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId,
            string folderId)
        {
            throw new NotImplementedException();
        }

        public override async Task<AccountInformation> GetAccountInformation(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            if (!(jobData is TwitterCrawlJobData twitterCrawlJobData))
            {
                throw new Exception("Wrong CrawlJobData type");
            }

            var client = _twitterClientFactory.CreateNew(twitterCrawlJobData);
            return await Task.FromResult(client.GetAccountInformation());
        }

        public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
        {
            return webHooksEnabled && ConfigurationManager.AppSettings.GetFlag("Feature.Webhooks.Enabled", false) ? $"{relativeDateTime.Minute} 0/23 * * *"
                : $"{relativeDateTime.Minute} 0/4 * * *";
        }

        public override Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition, [NotNull] IDictionary<string, object> config)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            throw new NotImplementedException();
        }

        public override Task<IEnumerable<WebhookDefinition>> GetWebHooks(ExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));

            throw new NotImplementedException();
        }

        public override IEnumerable<string> WebhookManagementEndpoints([NotNull] IEnumerable<string> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            if (!ids.Any())
            {
                throw new ArgumentException(nameof(ids));
            }

            throw new NotImplementedException();
        }

        public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));


            //There is no limit set, so you can pull as often and as much as you want.
            return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
        }

        // TODO Please see https://cluedin-io.github.io/CluedIn.Documentation/docs/1-Integration/build-integration.html
        public string Icon => TwitterConstants.IconResourceName;
        public string Domain { get; } = TwitterConstants.Uri;
        public string About { get; } = TwitterConstants.CrawlerDescription;
        public AuthMethods AuthMethods { get; } = TwitterConstants.AuthMethods;
        public IEnumerable<Control> Properties => null;
        public string ServiceType { get; } = JsonConvert.SerializeObject(TwitterConstants.ServiceType);
        public string Aliases { get; } = JsonConvert.SerializeObject(TwitterConstants.Aliases);
        public Guide Guide { get; set; } = new Guide
        {
            Instructions = TwitterConstants.Instructions,
            Value = new List<string> { TwitterConstants.CrawlerDescription },
            Details = TwitterConstants.Details

        };

        public string Details { get; set; } = TwitterConstants.Details;
        public string Category { get; set; } = TwitterConstants.Category;
        public new IntegrationType Type { get; set; } = TwitterConstants.Type;
    }
}
