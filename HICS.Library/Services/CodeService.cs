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
    public class CodeService : ICodeService
    {
        #region Properties and Constructor
        private HttpClient _apiClient;
        public HttpClient ApiClient
        {
            get { return _apiClient; }
        }

        public CodeService()
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri("https://localhost:44366/");
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region CRUD to API
        public async Task<List<Code>> Get()
        {
            using (HttpResponseMessage respone = await _apiClient.GetAsync("/api/Code"))
            {
                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsAsync<List<Code>>();
                    return result;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task<Code> GetById(int id)
        {
            using (HttpResponseMessage respone = await _apiClient.GetAsync($"/api/Code/{id}"))
            {
                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsAsync<Code>();
                    return result;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public async Task Post(Code code)
        {
            using (HttpResponseMessage response = await _apiClient.PostAsJsonAsync("/api/Code", code))
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

        public async Task Update(int id, Code code)
        {
            using (HttpResponseMessage response = await _apiClient.PutAsJsonAsync($"/api/Code/{id}", code))
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
            using (HttpResponseMessage response = await _apiClient.DeleteAsync($"/api/Code/{id}"))
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
