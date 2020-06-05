using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using HICS_Mobile.Services;
using Java.Net;
using Xamarin.Forms;


[assembly: Dependency(typeof(Notification))]
namespace HICS_Mobile.Droid
{
    public class Notification : INotification
    {
        private Context mContext;
        private NotificationManager mNotificationManager;
        private NotificationCompat.Builder mBuilder;
        public static string NOTIFICATION_CHANNEL_ID = "10023";

        public string getChannel()
        {
            return NOTIFICATION_CHANNEL_ID;
        }

        public Notification()
        {
            mContext = global::Android.App.Application.Context;
        }
        public void SendNewNotification(string title, string body)
        {
            try
            {
                var intent = new Intent(mContext, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra(title, body);
                var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.OneShot);

                mBuilder = new NotificationCompat.Builder(mContext, NOTIFICATION_CHANNEL_ID);

                mBuilder.SetContentTitle(title)
                        .SetSmallIcon(Resource.Drawable.ic_launcher)
                        .SetContentText(body)
                        .SetChannelId(NOTIFICATION_CHANNEL_ID)
                        .SetAutoCancel(true)
                        .SetShowWhen(false)
                        .SetContentIntent(pendingIntent);

                //var notificationManager = NotificationManager.FromContext(mContext);
                mNotificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;
                mNotificationManager.Notify(0, mBuilder.Build());
            }
            catch (Exception ex)
            {
                Log.Debug(MainActivity.TAG, ex.Message);
            }
        }
    }
}