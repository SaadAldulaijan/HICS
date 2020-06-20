using HICS.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public class MembershipService : IMembershipService
    {
        #region Properties and Constructor
        private HttpClient _apiClient;
        public HttpClient ApiClient
        {
            get { return _apiClient; }
        }

        public MembershipService()
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri("https://localhost:44366/");
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region CRUD to API
        public async Task<List<Membership>> Get()
        {
            using (HttpResponseMessage respone = await _apiClient.GetAsync("/api/Membership"))
            {
                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsAsync<List<Membership>>();
                    return result;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task Post(Membership membership)
        {
            using (HttpResponseMessage response = await _apiClient.PostAsJsonAsync("/api/Membership", membership))
            {
                if (response.IsSuccessStatusCode)
                {
                    // log this action
                    Debug.WriteLine($"Successfull operation .. {response.StatusCode}");
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task DeleteComposite(int firstKey,int secondKey)
        {
            using (HttpResponseMessage response = await _apiClient.DeleteAsync($"/api/Membership/{firstKey}/{secondKey}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfull operation .. {response.StatusCode}");
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        #endregion
    }
}
