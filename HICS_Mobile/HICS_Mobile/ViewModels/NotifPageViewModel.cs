using HICS_Mobile.Models;
using HICS_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HICS_Mobile.ViewModels
{
    public class NotifPageViewModel : BaseViewModel
    {
        #region Properties
        private string _status;
        private string _registrationId;
        private readonly RestService _restService;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string RegistrationId
        {
            get { return _registrationId; }
            set
            {
                _registrationId = value;
                OnPropertyChanged(nameof(_registrationId));
            }
        }
        #endregion

        #region Commands
        public ICommand GetRegistrationIdCommand { get; private set; }

        public ICommand RegisterForPushNotifications { get; private set; }

        public ICommand UnregisterFromPushNotifications { get; private set; }

        public ICommand SendPushNotification { get; private set; }
        #endregion

        #region Constructor
        public NotifPageViewModel()
        {
            _restService = new RestService();

            GetRegistrationIdCommand = new Command(async (parameter) =>
            {
                RegistrationId = await _restService.GetPushRegistrationId();
                Status = "Registration ID obtained: " + RegistrationId;
            });

            RegisterForPushNotifications = new Command(async (parameter) =>
            {

                var handle = "my_notification_channel";
                var tag = new List<string>() { "MainActivity" };
                var deviceUpdate = new DeviceRegistration()
                {
                    Handle = handle,
                    Platform = MobilePlatform.gcm,
                    Tags = tag.ToArray()
                };

                var result = await _restService.EnablePushNotifications(RegistrationId, deviceUpdate);
                if (result)
                    Status = "Successfuly enabled push notifications";
                else
                    Status = "Failed to enable push notifications";
            });

            UnregisterFromPushNotifications = new Command(async (parameter) =>
            {
                var result = await _restService.UnregisterFromNotifications(RegistrationId);
                if (result)
                    Status = "Successfuly unregistered from push notifications";
                else
                    Status = "Failed to unregister from push notifications";
            });

            SendPushNotification = new Command(async (parameter) =>
            {
                var msgBody = "Hi";
                var notification = new Notification()
                {
                    Content = "{\"data\":{\"message\": \"" + msgBody + "\"}}"
                };

                var result = await _restService.SendNotification(notification);
                if (result)
                    Status = "Successfuly sent push notifications";
                else
                    Status = "Failed to send push notifications";
            });
        }

        #endregion

        #region Methods
        //private async Task<string> InitNotificationsAsync()
        //{
        //    // Get a channel URI from WNS.
        //    var channel = await PushNotificationChannelManager
        //        .CreatePushNotificationChannelForApplicationAsync();



        //    channel.PushNotificationReceived += Channel_PushNotificationReceived;

        //    return channel.Uri;
        //}

        //private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        //{
        //    ShowToastNotification(args.ToastNotification);
        //}

        //private void ShowToastNotification(ToastNotification notification)
        //{
        //    ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
        //    notification.ExpirationTime = DateTime.Now.AddSeconds(4);
        //    ToastNotifier.Show(notification);
        //}
        #endregion
    }
}
