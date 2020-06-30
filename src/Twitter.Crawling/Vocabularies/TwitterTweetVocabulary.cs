using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Twitter.Vocabularies
{
    public class TwitterTweetVocabulary : SimpleVocabulary
    {
        public TwitterTweetVocabulary()
        {
            VocabularyName = "Twitter Tweet"; // TODO: Set value
            KeyPrefix = "twitter.tweet"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.Announcement; // TODO: Set value

            AddGroup("Twitter TwitterTweet Details", group =>
            {
                created_at = group.Add(new VocabularyKey("created_at", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                id = group.Add(new VocabularyKey("id", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                id_str = group.Add(new VocabularyKey("id_str", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                text = group.Add(new VocabularyKey("text", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                display_text_range = group.Add(new VocabularyKey("display_text_range", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                truncated = group.Add(new VocabularyKey("truncated", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                user = group.Add(new VocabularyKey("user", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                extended_tweet = group.Add(new VocabularyKey("extended_tweet", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                entities = group.Add(new VocabularyKey("entities", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                place = group.Add(new VocabularyKey("place", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));

            });
        }

        public VocabularyKey created_at { get; internal set; }
        public VocabularyKey id { get; internal set; }
        public VocabularyKey id_str { get; internal set; }
        public VocabularyKey text { get; internal set; }
        public VocabularyKey display_text_range { get; internal set; }
        public VocabularyKey truncated { get; internal set; }
        public VocabularyKey user { get; internal set; }
        public VocabularyKey extended_tweet { get; internal set; }
        public VocabularyKey entities { get; internal set; }
        public VocabularyKey place { get; internal set; }
    }
}
