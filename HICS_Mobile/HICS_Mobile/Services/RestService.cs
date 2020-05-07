using HICS_Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HICS_Mobile.Services
{
    public class RestService
    {
        private HttpClient _httpClient;
        private readonly string _serviceAddress = "https://localhost:44307/api/notifications";
        public RestService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetPushRegistrationId()
        {
            string registrationId = string.Empty;
            HttpResponseMessage response = await _httpClient.GetAsync(string.Concat(_serviceAddress, "/register"));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                registrationId = JsonConvert.DeserializeObject<string>(jsonResponse);
            }

            return registrationId;
        }

        //public async Task<List<User>> GetGroupMembers1(int groupId)
        //{
        //    using (HttpResponseMessage response = await _apiClient.GetAsync($"/api/User/GetGroupMembers?groupId={groupId}"))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<List<User>>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}


        public async Task<bool> UnregisterFromNotifications(string registrationId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(string.Concat(_serviceAddress, $"/unregister/{registrationId}"));
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> EnablePushNotifications(string id, DeviceRegistration deviceUpdate)
        {
            string registrationId = string.Empty;
            var content = new StringContent(JsonConvert.SerializeObject(deviceUpdate), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(string.Concat(_serviceAddress, $"/enable/{id}"), content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> SendNotification(Notification newNotification)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newNotification), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(string.Concat(_serviceAddress, "/send"), content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
