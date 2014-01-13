using Newtonsoft.Json;
using System;
using System.Globalization;

namespace RaphaelPinho.Twitter.Entities
{
    public class Tweet
    {
        const string format = "ddd MMM dd HH:mm:ss zzzz yyyy";

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("created_at")]
        public string CreatedAtUnformatted
        {
            set
            {
                CreatedAt = DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            }
        }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        public override bool Equals(object obj)
        {
            var person = obj as Tweet;

            return Equals(person);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Tweet tweet)
        {
            if (tweet == null) 
                return false;

            if (string.IsNullOrEmpty(tweet.Id))
                return false;

            return Id.Equals(tweet.Id);
        }
    }
}
