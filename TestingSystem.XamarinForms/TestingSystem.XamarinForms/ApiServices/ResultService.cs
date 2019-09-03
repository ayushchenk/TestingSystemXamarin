using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestingSystem.BusinessModel.Model;
using Xamarin.Forms;

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
            try
            {
                string result = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<IEnumerable<StudentTestResultDTO>>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<IEnumerable<StudentTestResultDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public async Task PostAsync(StudentTestResultDTO result)
        {
            try
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
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
    }
}
