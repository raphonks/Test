using Newtonsoft.Json;
using System.Collections.Generic;

namespace RaphaelPinho.Twitter.Communications
{
    public class SearchResponse
    {
        [JsonProperty("statuses")]
        public List<Entities.Tweet> Statuses { get; set; }
    }
}
