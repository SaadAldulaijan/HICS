using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HICS_Mobile.Droid
{
    public static class Constants
    {
        public const string ListenConnectionString = "Endpoint=sb://hics-notificationhubns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=UVcZg/KHqsStXdmqrcQFQq6TpGtkUJHFr2ClXi8pbOc=";
        public const string NotificationHubName = "HICS-NotificationHub";
    }
}