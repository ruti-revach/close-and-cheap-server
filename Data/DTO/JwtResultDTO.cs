using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Data.DTO
{
    public class JwtResultDTO
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "expired")]
        public long Expired { get; set; }
    }
}
