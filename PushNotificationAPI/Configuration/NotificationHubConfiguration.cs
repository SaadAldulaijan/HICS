using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotificationAPI.Configuration
{
    public class NotificationHubConfiguration
    {
        public string ConnectionString { get; set; }
        public string HubName { get; set; }
    }
}
