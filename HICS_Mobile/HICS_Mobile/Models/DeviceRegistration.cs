using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HICS_Mobile.Models
{
    public class DeviceRegistration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MobilePlatform Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }
}
