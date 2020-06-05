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
using Android.Util;
using Firebase.Messaging;
using Android.Support.V4.App;
using WindowsAzure.Messaging;
using HICS_Mobile.Services;
using System.Threading.Tasks;
using HICS_Mobile.Models;
using Xamarin.Forms.Internals;

namespace HICS_Mobile.Droid
{
    /// <summary>
    /// My target is to have methods like this: 
    /// 
    /// SendNotification(string userId, string title, string body); return notificationId;
    /// SendNotification(List<string> userId, string title, string body); return List<string> notificationId; 
    /// 
    /// 
    /// </summary>


    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        NotificationHub hub;
        RestService _rest;

        //this is called when a notification is received.
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            new Notification().SendNewNotification(message.GetNotification().Title, message.GetNotification().Body);
            //Android.Util.Log.Debug(TAG, "From: " + message.From);
            //if (message.GetNotification() != null)
            //{
            //    //These is how most messages will be received
            //    Android.Util.Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            //    SendNotification(message.GetNotification().Body);
            //}
            //else
            //{
            //    //Only used for debugging payloads sent from the Azure portal
            //    SendNotification(message.Data.Values.First());

            //}
        }



        //void SendNotification(string messageBody)
        //{
        //    var intent = new Intent(this, typeof(MainActivity));
        //    intent.AddFlags(ActivityFlags.ClearTop);
        //    var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

        //    var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID);

        //    notificationBuilder.SetContentTitle("Test")
        //            .SetSmallIcon(Resource.Drawable.ic_launcher)
        //            .SetContentText(messageBody)
        //            .SetAutoCancel(true)
        //            .SetShowWhen(false)
        //            .SetContentIntent(pendingIntent);

        //    var notificationManager = NotificationManager.FromContext(this);

        //    notificationManager.Notify(0, notificationBuilder.Build());
        //}

        //this will be called when an application first time installed.
        //this will generate token ID for android, should be sent to azure.
        public override void OnNewToken(string token)
        {
            Android.Util.Log.Debug(TAG, "FCM token: " + token);
            SendRegistrationToServer(token).Wait();
        }

        async Task SendRegistrationToServer(string token)
        {
            _rest = new RestService();
            var notification = new Notification();
            string channel = notification.getChannel();
            var regId = await _rest.GetPushRegistrationId();
            var tags = new List<string>() { };
            var device = new DeviceRegistration()
            {
                Handle = channel,
                Platform = MobilePlatform.gcm,
                Tags = tags.ToArray()
            };

            bool enable = await _rest.EnablePushNotifications(regId, device);
            if (enable)
            {
                Android.Util.Log.Debug(TAG, "ENABLED SUCCESSFULLY");
            }
            else
            {
                Android.Util.Log.Debug(TAG, "What happen: " + regId + token);

            }
            //// Register with Notification Hubs
            //hub = new NotificationHub(Constants.NotificationHubName,
            //                            Constants.ListenConnectionString, this);

            //var tags = new List<string>() { };
            ////i have to call api here.
            //var regID = hub.Register(token, tags.ToArray()).RegistrationId;

            //Android.Util.Log.Debug(TAG, $"Successful registration of ID {regID}");
        }
    }
}