using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Twitter.Core;
using CluedIn.Crawling.Twitter.Core.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CluedIn.Crawling.Twitter.Infrastructure
{
    // TODO - This class should act as a client to retrieve the data to be crawled.
    // It should provide the appropriate methods to get the data
    // according to the type of data source (e.g. for AD, GetUsers, GetRoles, etc.)
    // It can receive a IRestClient as a dependency to talk to a RestAPI endpoint.
    // This class should not contain crawling logic (i.e. in which order things are retrieved)
    public class TwitterClient
    {
        private const string BaseUri = "http://sample.com";

        private readonly ILogger log;

        private readonly IRestClient client;

        public TwitterClient(ILogger log, TwitterCrawlJobData twitterCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
        {
            if (twitterCrawlJobData == null)
            {
                throw new ArgumentNullException(nameof(twitterCrawlJobData));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.client = client ?? throw new ArgumentNullException(nameof(client));

            // TODO use info from twitterCrawlJobData to instantiate the connection
            client.BaseUrl = new Uri(BaseUri);
            client.AddDefaultParameter("api_key", twitterCrawlJobData.ApiKey, ParameterType.QueryString);
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var request = new RestRequest(url, Method.GET);

            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var diagnosticMessage = $"Request to {client.BaseUrl}{url} failed, response {response.ErrorMessage} ({response.StatusCode})";
                log.Error(() => diagnosticMessage);
                throw new InvalidOperationException($"Communication to jsonplaceholder unavailable. {diagnosticMessage}");
            }

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation("", "");
        }

        public User GetUser(string token, string screenName)
        {
            var userFormat = "https://api.twitter.com/1.1/users/show.json?screen_name={0}&include_entities=false";
            var userUrl = string.Format(userFormat, screenName);
            HttpWebRequest userRequest = (HttpWebRequest)WebRequest.Create(userUrl);
            var userHeaderFormat = "{0} {1}";
            userRequest.Headers.Add("Authorization", string.Format(userHeaderFormat, "bearer", token));
            userRequest.Method = "Get";
            WebResponse userResponse = userRequest.GetResponse();
            var userJson = string.Empty;
            using (userResponse)
            {
                using (var reader = new StreamReader(userResponse.GetResponseStream()))
                {
                    userJson = reader.ReadToEnd();
                }
            }
            return JsonConvert.DeserializeObject<User>(userJson);
        }

        public IEnumerable<User> GetFollowers(string token, string screenName)
        {
            long cursor = -1;
            while (cursor != 0)
            {
                var followersFormat = "https://api.twitter.com/1.1/followers/list.json?screen_name={0}&count=10";
                var followersUrl = string.Format(followersFormat, screenName);
                HttpWebRequest followersRequest = (HttpWebRequest)WebRequest.Create(followersUrl);
                var followersHeaderFormat = "{0} {1}";
                followersRequest.Headers.Add("Authorization", string.Format(followersHeaderFormat, "bearer", token));
                followersRequest.Method = "Get";
                WebResponse followersResponse = followersRequest.GetResponse();
                var followersJson = string.Empty;
                using (followersResponse)
                {
                    using (var reader = new StreamReader(followersResponse.GetResponseStream()))
                    {
                        followersJson = reader.ReadToEnd();
                    }
                }
                var followers = JsonConvert.DeserializeObject<Followers>(followersJson);
                cursor = long.Parse(followers.next_cursor);
                foreach (var item in followers.users)
                {
                    yield return item;
                }
            }
        }
        //TODO check permissions, can't access mentions with the current keys
        //public IEnumerable<Mention> GetMentions(string token)
        //{
        //    //TODO: change things from timeline to mentions
        //    var timelineFormat = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";
        //    var timelineUrl = string.Format(timelineFormat, "cluedinhq");
        //    HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(timelineUrl);
        //    var timelineHeaderFormat = "{0} {1}";
        //    timeLineRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, "bearer", token));
        //    timeLineRequest.Method = "Get";
        //    WebResponse timeLineResponse = timeLineRequest.GetResponse();
        //    var timeLineJson = string.Empty;
        //    using (timeLineResponse)
        //    {
        //        using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
        //        {
        //            timeLineJson = reader.ReadToEnd();
        //        }
        //    }
        //    var Tweets = JsonConvert.DeserializeObject<List<Followers>>(timeLineJson);
        //    foreach (var item in Tweets)
        //    {
        //        foreach (var user in item.users)
        //        {
        //            yield return user;
        //        }
        //    }
        //}

        public IEnumerable<Tweet> GetTweets(string token, string screenName)
        {
                var timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&include_rts=1&exclude_replies=1&count=10&trim_user=1&cursor={1}";
                var timelineUrl = string.Format(timelineFormat, screenName);
                HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(timelineUrl);
                var timelineHeaderFormat = "{0} {1}";
                timeLineRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, "bearer", token));
                timeLineRequest.Method = "Get";
                WebResponse timeLineResponse = timeLineRequest.GetResponse();
                var timeLineJson = string.Empty;
                using (timeLineResponse)
                {
                    using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
                    {
                        timeLineJson = reader.ReadToEnd();
                    }
                }
                var Tweets = JsonConvert.DeserializeObject<List<Tweet>>(timeLineJson);
                foreach (var item in Tweets)
                {
                    yield return item;
                }
            //return null;
        }
    }
}
