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
using CluedIn.Core.Data.Parts;
using RestSharp;

namespace CluedIn.Crawling.Twitter.ClueProducers
{
    public class UserProducer : BaseClueProducer<User>
    {
        private readonly IClueFactory _factory;

        public UserProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] User input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Infrastructure.User, input.id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.screen_name))
            {
                data.Codes.Add(new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("Twitter"), input.screen_name.ToLowerInvariant()));
            }

            if (!string.IsNullOrWhiteSpace(input.screen_name))
            {
                data.Name = input.name.ToString();
            }

            if (!string.IsNullOrWhiteSpace(input.description))
            {
                data.Description = input.description.ToString();
            }

            if (!string.IsNullOrEmpty(input.created_at))
            {
                if (DateTimeOffset.TryParse(input.created_at, out var createdDate))
                {
                    data.CreatedDate = createdDate;
                }
            }

            if (input.profile_image_url != null)
            {
                RawDataPart rawDataPart = null;

                RestClient restClient = new RestClient(input.profile_image_url);
                var d = restClient.DownloadData(new RestRequest(Method.GET));
                rawDataPart = new RawDataPart
                {
                    Type = "/RawData/PreviewImage",
                    MimeType = CluedIn.Core.FileTypes.MimeType.Jpeg.Code,
                    FileName = "preview_{0}".FormatWith(input.profile_image_url),
                    RawDataMD5 = FileHashUtility.GetMD5Base64String(d),
                    RawData = Convert.ToBase64String(d)
                };

                if (rawDataPart != null)
                {
                    clue.Details.RawData.Add(rawDataPart);
                    clue.Data.EntityData.PreviewImage = new ImageReferencePart(rawDataPart, 255, 255);
                }
            }

            if (!string.IsNullOrWhiteSpace(input.following_user))
            {
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.Follows, input, input.following_user.ToLowerInvariant());
            }

            //if (!string.IsNullOrWhiteSpace(input.id))
            //{
            //    _factory.CreateOutgoingEntityReference(clue, EntityType.Announcement, EntityEdgeType.Created, input, input.id);
            //}

            var vocab = new TwitterUserVocabulary();

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            data.Properties[vocab.id] = input.id.PrintIfAvailable();
            data.Properties[vocab.id_str] = input.id_str.PrintIfAvailable();
            data.Properties[vocab.name] = input.name.PrintIfAvailable();
            data.Properties[vocab.screen_name] = input.screen_name.PrintIfAvailable();
            data.Properties[vocab.location] = input.location.PrintIfAvailable();
            data.Properties[vocab.profile_location] = input.profile_location.PrintIfAvailable();
            data.Properties[vocab.description] = input.description.PrintIfAvailable();
            data.Properties[vocab.url] = input.url.PrintIfAvailable();
            //data.Properties[vocab.entities] = input.entities.PrintIfAvailable();
            data.Properties[vocab.isProtected] = input.isProtected.PrintIfAvailable();
            data.Properties[vocab.followers_count] = input.followers_count.PrintIfAvailable();
            data.Properties[vocab.friends_count] = input.friends_count.PrintIfAvailable();
            data.Properties[vocab.listed_count] = input.listed_count.PrintIfAvailable();
            data.Properties[vocab.created_at] = input.created_at.PrintIfAvailable();
            data.Properties[vocab.favourites_count] = input.favourites_count.PrintIfAvailable();
            data.Properties[vocab.utc_offset] = input.utc_offset.PrintIfAvailable();
            data.Properties[vocab.time_zone] = input.time_zone.PrintIfAvailable();
            data.Properties[vocab.geo_enabled] = input.geo_enabled.PrintIfAvailable();
            data.Properties[vocab.verified] = input.verified.PrintIfAvailable();
            data.Properties[vocab.statuses_count] = input.statuses_count.PrintIfAvailable();
            data.Properties[vocab.lang] = input.lang.PrintIfAvailable();
            data.Properties[vocab.contributors_enabled] = input.contributors_enabled.PrintIfAvailable();
            data.Properties[vocab.is_translator] = input.is_translator.PrintIfAvailable();
            data.Properties[vocab.is_translation_enabled] = input.is_translation_enabled.PrintIfAvailable();
            data.Properties[vocab.profile_background_color] = input.profile_background_color.PrintIfAvailable();
            data.Properties[vocab.profile_background_image_url] = input.profile_background_image_url.PrintIfAvailable();
            data.Properties[vocab.profile_background_image_url_https] = input.profile_background_image_url_https.PrintIfAvailable();
            data.Properties[vocab.profile_background_tile] = input.profile_background_tile.PrintIfAvailable();
            data.Properties[vocab.profile_image_url] = input.profile_image_url.PrintIfAvailable();
            data.Properties[vocab.profile_image_url_https] = input.profile_image_url_https.PrintIfAvailable();
            data.Properties[vocab.profile_banner_url] = input.profile_banner_url.PrintIfAvailable();
            data.Properties[vocab.profile_link_color] = input.profile_link_color.PrintIfAvailable();
            data.Properties[vocab.profile_sidebar_border_color] = input.profile_sidebar_border_color.PrintIfAvailable();
            data.Properties[vocab.profile_sidebar_fill_color] = input.profile_sidebar_fill_color.PrintIfAvailable();
            data.Properties[vocab.profile_text_color] = input.profile_text_color.PrintIfAvailable();
            data.Properties[vocab.profile_use_background_image] = input.profile_use_background_image.PrintIfAvailable();
            data.Properties[vocab.has_extended_profile] = input.has_extended_profile.PrintIfAvailable();
            data.Properties[vocab.default_profile] = input.default_profile.PrintIfAvailable();
            data.Properties[vocab.default_profile_image] = input.default_profile_image.PrintIfAvailable();
            data.Properties[vocab.following] = input.following.PrintIfAvailable();
            data.Properties[vocab.follow_request_sent] = input.follow_request_sent.PrintIfAvailable();
            data.Properties[vocab.notifications] = input.notifications.PrintIfAvailable();
            data.Properties[vocab.translator_type] = input.translator_type.PrintIfAvailable();

            return clue;
        }
    }
}
