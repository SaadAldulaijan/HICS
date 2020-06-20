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
    public class GroupService : IGroupService
    {
        #region Properties and Constructor
        private HttpClient _apiClient;
        public HttpClient ApiClient
        {
            get { return _apiClient; }
        }

        public GroupService()
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri("https://localhost:44366/");
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region CRUD to API
        public async Task<List<Group>> Get()
        {
            using (HttpResponseMessage respone = await _apiClient.GetAsync("/api/Group"))
            {
                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsAsync<List<Group>>();
                    return result;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task<Group> GetById(int id)
        {
            using (HttpResponseMessage respone = await _apiClient.GetAsync($"/api/Group/{id}"))
            {
                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsAsync<Group>();
                    return result;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task Post(Group group)
        {
            using (HttpResponseMessage response = await _apiClient.PostAsJsonAsync("/api/Group", group))
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

        public async Task Update(int id, Group group)
        {
            using (HttpResponseMessage response = await _apiClient.PutAsJsonAsync($"/api/Group/{id}", group))
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
        public async Task Delete(int id)
        {
            using (HttpResponseMessage response = await _apiClient.DeleteAsync($"/api/Group/{id}"))
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
