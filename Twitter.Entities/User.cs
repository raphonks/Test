using Newtonsoft.Json;

namespace RaphaelPinho.Twitter.Entities
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}
