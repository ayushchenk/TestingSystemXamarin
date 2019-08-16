using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.XamarinForms.Models;

namespace TestingSystem.XamarinForms.ApiServices
{
    public class QuickTestService
    {
        private const string url = "https://testingsystemapplication.azurewebsites.net/api/quicktestapi";
        private HttpClient client;

        public QuickTestService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public ParticipateViewModel Get(QuickTestApiModel model)
        {
            var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("SubjectId", model.SubjectId.ToString()),
                new KeyValuePair<string, string>("QuestionCount", model.QuestionCount.ToString())
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new FormUrlEncodedContent(keyValues);

            var response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ParticipateViewModel>(response.Content.ReadAsStringAsync().Result);
            else
                return null;
        }

        public Task<ParticipateViewModel> GetAsync(QuickTestApiModel model)
        {
            return Task.Run(() => Get(model));
        }
    }
}
