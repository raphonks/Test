using Newtonsoft.Json;

namespace RaphaelPinho.Twitter.Entities
{
    public class UserMention
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}
