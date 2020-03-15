using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.DTO
{
    public class TokenDTO
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiratiion")]
        public int RefreshExpiratiion { get; set; }
    }
}
