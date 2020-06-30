using System.Collections.Generic;
using CluedIn.Crawling.Twitter.Core;

namespace CluedIn.Crawling.Twitter.Integration.Test
{
  public static class TwitterConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { TwitterConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
