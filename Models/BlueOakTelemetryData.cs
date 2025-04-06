using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMQTTWindows.Models
{
    public class BlueOakTelemetryData
    {
        [JsonPropertyName("t-data")]
        public string TData { get; set; }
        [JsonPropertyName("f-data")]
        public string FData { get; set; }
    }
}
