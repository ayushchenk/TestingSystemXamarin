using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.XamarinForms.Models;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ApiServices
{
    public class ParticipateService
    {
        private const string url = "https://testingsystemapplication.azurewebsites.net/api/participateapi";
        private HttpClient client;

        public ParticipateService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Services.Service.SetBasicAuthHeader(client);
        }

        public ParticipateViewModel Get(int id)
        {
            try
            {
                string result = client.GetStringAsync(url + "/" + id).Result;
                return JsonConvert.DeserializeObject<ParticipateViewModel>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<ParticipateViewModel> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }
    }
}
