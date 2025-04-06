using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMQTTWindows.Models
{
    public class ThingsBoardTelemetryData
    {
        [JsonPropertyName("ts")]
        public long Ts { get; set; }

        [JsonPropertyName("values")]
        public Object Values { get; set; }
    }
}
