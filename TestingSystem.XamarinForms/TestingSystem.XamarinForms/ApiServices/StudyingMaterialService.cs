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
    public class StudyingMaterialService
    {
        private const string url = "https://testingsystemapplication.azurewebsites.net/api/studyingmaterialsapi";
        private HttpClient client;

        public StudyingMaterialService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Services.Service.SetBasicAuthHeader(client);
        }

        public IEnumerable<StudyingMaterialDTO> GetAll()
        {
            try
            {

            string result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<StudyingMaterialDTO>>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<IEnumerable<StudyingMaterialDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }
    }
}
