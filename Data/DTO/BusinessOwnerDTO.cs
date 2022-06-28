using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Data.DTO
{
    public class BusinessOwnerDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "categoryid")]
        public int CategoryId { get; set; }
    }
}
