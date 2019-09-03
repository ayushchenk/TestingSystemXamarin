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
    public class SpecializationService
    {
        const string url = "https://testingsystemapplication.azurewebsites.net/api/specializationapi";
        private HttpClient client;

        public SpecializationService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public IEnumerable<SpecializationDTO> GetAll()
        {
            try
            {
                string result = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<IEnumerable<SpecializationDTO>>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<IEnumerable<SpecializationDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public SpecializationDTO Get(int id)
        {
            try
            {
                string result = client.GetStringAsync(url + "/" + id).Result;
                return JsonConvert.DeserializeObject<SpecializationDTO>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<SpecializationDTO> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }
    }
}
