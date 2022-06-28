using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Data.DTO
{
    public class UserDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }       
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address{ get; set; }
    }
}
