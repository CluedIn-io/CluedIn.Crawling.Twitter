using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Twitter.Vocabularies
{
    public class TwitterUserVocabulary : SimpleVocabulary
    {

        public TwitterUserVocabulary()
        {
            this.VocabularyName = "Twitter User";
            this.KeyPrefix = "twitter.user";
            this.KeySeparator = ".";
            this.Grouping = EntityType.Infrastructure.User;

            this.AddGroup("Twitter Message Details", group =>
            {
                id = group.Add(new VocabularyKey("id", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                id_str = group.Add(new VocabularyKey("id_str", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                name = group.Add(new VocabularyKey("name", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                screen_name = group.Add(new VocabularyKey("screen_name", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                location = group.Add(new VocabularyKey("location", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_location = group.Add(new VocabularyKey("profile_location", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                description = group.Add(new VocabularyKey("description", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                url = group.Add(new VocabularyKey("url", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                entities = group.Add(new VocabularyKey("entities", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                isProtected = group.Add(new VocabularyKey("isProtected", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                followers_count = group.Add(new VocabularyKey("followers_count", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                friends_count = group.Add(new VocabularyKey("friends_count", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                listed_count = group.Add(new VocabularyKey("listed_count", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                created_at = group.Add(new VocabularyKey("created_at", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                favourites_count = group.Add(new VocabularyKey("favourites_count", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                utc_offset = group.Add(new VocabularyKey("utc_offset", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                time_zone = group.Add(new VocabularyKey("time_zone", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                geo_enabled = group.Add(new VocabularyKey("geo_enabled", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                verified = group.Add(new VocabularyKey("verified", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                statuses_count = group.Add(new VocabularyKey("statuses_count", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                lang = group.Add(new VocabularyKey("lang", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                contributors_enabled = group.Add(new VocabularyKey("contributors_enabled", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                is_translator = group.Add(new VocabularyKey("is_translator", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                is_translation_enabled = group.Add(new VocabularyKey("is_translation_enabled", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_background_color = group.Add(new VocabularyKey("profile_background_color", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_background_image_url = group.Add(new VocabularyKey("profile_background_image_url", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                profile_background_image_url_https = group.Add(new VocabularyKey("profile_background_image_url_https", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                profile_background_tile = group.Add(new VocabularyKey("profile_background_tile", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_image_url = group.Add(new VocabularyKey("profile_image_url", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                profile_image_url_https = group.Add(new VocabularyKey("profile_image_url_https", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                profile_banner_url = group.Add(new VocabularyKey("profile_banner_url", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                profile_link_color = group.Add(new VocabularyKey("profile_link_color", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_sidebar_border_color = group.Add(new VocabularyKey("profile_sidebar_border_color", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_sidebar_fill_color = group.Add(new VocabularyKey("profile_sidebar_fill_color", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_text_color = group.Add(new VocabularyKey("profile_text_color", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                profile_use_background_image = group.Add(new VocabularyKey("profile_use_background_image", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                has_extended_profile = group.Add(new VocabularyKey("has_extended_profile", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                default_profile = group.Add(new VocabularyKey("default_profile", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                default_profile_image = group.Add(new VocabularyKey("default_profile_image", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                following = group.Add(new VocabularyKey("following", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                follow_request_sent = group.Add(new VocabularyKey("follow_request_sent", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                notifications = group.Add(new VocabularyKey("notifications", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                translator_type = group.Add(new VocabularyKey("translator_type", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            });
        }

        public VocabularyKey id { get; set; }
        public VocabularyKey id_str { get; set; }
        public VocabularyKey name { get; set; }
        public VocabularyKey screen_name { get; set; }
        public VocabularyKey location { get; set; }
        public VocabularyKey profile_location { get; set; }
        public VocabularyKey description { get; set; }
        public VocabularyKey url { get; set; }
        public VocabularyKey entities { get; set; }
        public VocabularyKey isProtected { get; set; }
        public VocabularyKey followers_count { get; set; }
        public VocabularyKey friends_count { get; set; }
        public VocabularyKey listed_count { get; set; }
        public VocabularyKey created_at { get; set; }
        public VocabularyKey favourites_count { get; set; }
        public VocabularyKey utc_offset { get; set; }
        public VocabularyKey time_zone { get; set; }
        public VocabularyKey geo_enabled { get; set; }
        public VocabularyKey verified { get; set; }
        public VocabularyKey statuses_count { get; set; }
        public VocabularyKey lang { get; set; }
        public VocabularyKey contributors_enabled { get; set; }
        public VocabularyKey is_translator { get; set; }
        public VocabularyKey is_translation_enabled { get; set; }
        public VocabularyKey profile_background_color { get; set; }
        public VocabularyKey profile_background_image_url { get; set; }
        public VocabularyKey profile_background_image_url_https { get; set; }
        public VocabularyKey profile_background_tile { get; set; }
        public VocabularyKey profile_image_url { get; set; }
        public VocabularyKey profile_image_url_https { get; set; }
        public VocabularyKey profile_banner_url { get; set; }
        public VocabularyKey profile_link_color { get; set; }
        public VocabularyKey profile_sidebar_border_color { get; set; }
        public VocabularyKey profile_sidebar_fill_color { get; set; }
        public VocabularyKey profile_text_color { get; set; }
        public VocabularyKey profile_use_background_image { get; set; }
        public VocabularyKey has_extended_profile { get; set; }
        public VocabularyKey default_profile { get; set; }
        public VocabularyKey default_profile_image { get; set; }
        public VocabularyKey following { get; set; }
        public VocabularyKey follow_request_sent { get; set; }
        public VocabularyKey notifications { get; set; }
        public VocabularyKey translator_type { get; set; }

    }
}
