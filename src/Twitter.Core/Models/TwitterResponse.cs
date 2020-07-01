using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Dynamic;
using EasyNetQ.Events;

namespace CluedIn.Crawling.Twitter.Core.Models
{
    public class TwitterResponse
    {
        public TwitterResponse()
        {

        }
        public string id { get; set; }
        public List<User> Followers { get; set; }
        public List<string> Friends { get; set; }
        public List<Mention> Mentions { get; set; }
        public List<Tweet> Tweets { get; set; }

    }
    public class Mention
    {
        public Coordinates coordinates { get; set; }
        public string favorited { get; set; }
        public string truncated { get; set; }
        public string created_at { get; set; }
        public string id_str { get; set; }
        public Entities entities { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string contributors { get; set; }
        public string text { get; set; }
        public string retweet_count { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public string id { get; set; }
        public Geo geo { get; set; }
        public string retweeted { get; set; }
        public string in_reply_to_user_id { get; set; }
        public string place { get; set; }
        public User user { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public string source { get; set; }
        public string in_reply_to_status_id { get; set; }
        public string screen_name { get; set; }
        public string name { get; set; }
        public int[] indices { get; set; }
    }
    public class Geo
    {
        public Coordinates coordinates { get; set; }
        public string type { get; set; }
    }
    public class Coordinates
    {
        public float[] coordinates { get; set; }
    }

    public class Place
    {

    }
    public class Tweet
    {
        public string created_at { get; set; }
        public string id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string display_text_range { get; set; }
        public string truncated { get; set; }
        public User user { get; set; }
        public ExtendedTweet extended_tweet { get; set; }
        public Entities entities { get; set; }
        public Place place { get; set; }
        public string createdByScreenName { get; set; }
    }
    public class ProfileLocation
    {
        public string id { get; set; }
        public string url { get; set; }
        public string place_type { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string country_code { get; set; }
        public string country { get; set; }
    }
    public class User
    {
        public string following_user { get; set; }
        public string id { get; set; }
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public ProfileLocation profile_location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public Entities entities { get; set; }
        public string isProtected { get; set; }
        public string followers_count { get; set; }
        public string friends_count { get; set; }
        public string listed_count { get; set; }
        public string created_at { get; set; }
        public string favourites_count { get; set; }
        public string utc_offset { get; set; }
        public string time_zone { get; set; }
        public string geo_enabled { get; set; }
        public string verified { get; set; }
        public string statuses_count { get; set; }
        public string lang { get; set; }
        public string contributors_enabled { get; set; }
        public string is_translator { get; set; }
        public string is_translation_enabled { get; set; }
        public string profile_background_color { get; set; }
        public string profile_background_image_url { get; set; }
        public string profile_background_image_url_https { get; set; }
        public string profile_background_tile { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_banner_url { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public string profile_use_background_image { get; set; }
        public string has_extended_profile { get; set; }
        public string default_profile { get; set; }
        public string default_profile_image { get; set; }
        public string following { get; set; }
        public string follow_request_sent { get; set; }
        public string notifications { get; set; }
        public string translator_type { get; set; }

    }
    public class ExtendedTweet
    {
        public string full_text { get; set; }
        public string display_text_range { get; set; }
        public Entities entities { get; set; }
        public string truncated { get; set; }

    }
    public class Entities
    {
        public List<Url> urls { get; set; }
        public List<Hashtag> hashtags { get; set; }
        public List<Mention> user_mentions { get; set; }
    }
    public class Hashtag
    {
        public string text { get; set; }
        public int[] indices { get; set; }
    }
    public class Url
    {
        public string url { get; set; }
        public string expanded_url { get; set; }
        public string display_url { get; set; }
        public int[] indices { get; set; }
    }
    public class Followers
    {
        public IList<User> users { get; set; }
        public string next_cursor { get; set; }
        public string next_cursor_str { get; set; }
        public string previous_cursor { get; set; }
        public string previous_cursor_str { get; set; }
    }
}
