using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    public class MetaData
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = "69.9 Your Station Name";

        [JsonProperty("fm")]
        public float Fm { get; set; } = 69.9f;

        [JsonProperty("volume")]
        public float Volume { get; set; } = 1.0f;

        [JsonProperty("icon")]
        public string Icon { get; set; } = "UIIcon.alcohol_absynth";

        [JsonProperty("customIcon")]
        public CustomIcon CustomIcon { get; set; } = new();

        [JsonProperty("streamInfo")]
        public StreamInfo StreamInfo { get; set; } = new();

        [JsonProperty("order")]
        public string[] Order { get; set; } = [];
    }
}