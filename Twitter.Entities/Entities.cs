using Newtonsoft.Json;
using System.Collections.Generic;

namespace RaphaelPinho.Twitter.Entities
{
    public class Entities
    {
        [JsonProperty("user_mentions")]
        public List<UserMention> UserMentions { get; set; }
    }
}
