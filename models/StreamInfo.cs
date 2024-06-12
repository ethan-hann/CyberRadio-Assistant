using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    public class StreamInfo
    {
        [JsonProperty("streamURL")]
        public string StreamURL { get; set; } = "https://radio.garden/api/ara/content/listen/TP8NDBv7/channel.mp3";

        [JsonProperty("isStream")]
        public bool IsStream { get; set; } = false;
    }
}
