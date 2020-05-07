using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotificationAPI.NotificationHubs
{
    public class Notification : DeviceRegistration
    {
        public string Content { get; set; }
    }
}
