using System;
using Newtonsoft.Json;

namespace App.Controllers.Resources
{
    // Provides different structure for the returned token based on user's needs
    public class TokenResource
    {
        public string Username { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "expires", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "id_token", NullValueHandling = NullValueHandling.Ignore)]
        public string IdToken { get; set; }
    }
}