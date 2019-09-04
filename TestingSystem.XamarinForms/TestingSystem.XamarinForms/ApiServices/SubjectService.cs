using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ApiServices
{
    public class SubjectService
    {
        const string url = "https://testingsystemapplication.azurewebsites.net/api/subjectapi";
        private HttpClient client;

        public SubjectService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Services.Service.SetBasicAuthHeader(client);
        }

        public IEnumerable<SubjectDTO> GetAll()
        {
            try
            {
                string result = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public SubjectDTO Get(int id)
        {
            try
            {
            string result = client.GetStringAsync(url + "/" + id).Result;
            return JsonConvert.DeserializeObject<SubjectDTO>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<SubjectDTO> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }
    }
}
