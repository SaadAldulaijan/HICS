using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PushNotificationAPI.NotificationHubs
{
    public class DeviceRegistration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MobilePlatform Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }
}
