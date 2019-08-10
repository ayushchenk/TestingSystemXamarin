using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestingSystem.BusinessModel.Model;

namespace TestingSystem.XamarinForms.ApiServices
{
    public class ResultService
    {
        private const string url = "https://testingsystemapplication.azurewebsites.net/api/resultapi";
        private HttpClient client;

        public ResultService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public IEnumerable<StudentTestResultDTO> GetAll()
        {
            string result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<StudentTestResultDTO>>(result);
        }

        public Task<IEnumerable<StudentTestResultDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public async Task Post(StudentTestResultDTO result)
        {
            var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Result", result.Result.ToString()),
                new KeyValuePair<string, string>("GroupInTestId", result.GroupInTestId.ToString()),
                new KeyValuePair<string, string>("StudentId", result.StudentId.ToString()),
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new FormUrlEncodedContent(keyValues);

            var response = await client.SendAsync(request);
        }
    }
}
