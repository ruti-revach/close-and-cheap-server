using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Data.DTO
{
    public class AuthUserDTO
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
