using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;

using CluedIn.Crawling.Twitter.Vocabularies;
using CluedIn.Crawling.Twitter.Core.Models;
using System.Data;
using CluedIn.Crawling.Twitter.Infrastructure;
using System.Linq;

namespace CluedIn.Crawling.Twitter.ClueProducers
{
    public class TwitterTweetProducer : BaseClueProducer<Tweet>
    {
        private readonly IClueFactory _factory;

        public TwitterTweetProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Tweet input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Announcement, input.id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrEmpty(input.text))
            {
                data.Name = input.text.ToString();
            }

            if (!string.IsNullOrEmpty(input.created_at))
            {
                if (DateTimeOffset.TryParse(input.created_at, out var createdDate))
                {
                    data.CreatedDate = createdDate;
                }
            }


            if (!string.IsNullOrEmpty(input.createdByScreenName))
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.Created, input, input.createdByScreenName.ToLowerInvariant());

            var vocab = new TwitterTweetVocabulary();

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            data.Properties[vocab.created_at] = input.created_at.PrintIfAvailable();
            data.Properties[vocab.id] = input.id.PrintIfAvailable();
            data.Properties[vocab.id_str] = input.id_str.PrintIfAvailable();
            data.Properties[vocab.text] = input.text.PrintIfAvailable();
            data.Properties[vocab.display_text_range] = input.display_text_range.PrintIfAvailable();
            data.Properties[vocab.truncated] = input.truncated.PrintIfAvailable();
            data.Properties[vocab.user] = input.user.id.PrintIfAvailable();
            data.Properties[vocab.extended_tweet] = input.extended_tweet.PrintIfAvailable();
            if (input.entities != null)
            {
                if (input.entities.urls.Count > 0)
                    data.Properties[vocab.url] = input.entities.urls.Last().url;
            }
            //data.Properties[vocab.entities] = input.entities.PrintIfAvailable();
            //data.Properties[vocab.place] = input.place.PrintIfAvailable();

            return clue;
        }
    }
}
