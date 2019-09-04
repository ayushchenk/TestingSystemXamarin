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
    public class StudentService
    {
        const string url = "https://testingsystemapplication.azurewebsites.net/api/studentapi";
        private HttpClient client;

        public StudentService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Services.Service.SetBasicAuthHeader(client);
        }

        public IEnumerable<StudentDTO> GetAll()
        {
            try
            {
                string result = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<IEnumerable<StudentDTO>>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public StudentDTO Get(int id)
        {
            try
            {
                string result = client.GetStringAsync(url + "/" + id).Result;
                return JsonConvert.DeserializeObject<StudentDTO>(result);
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<StudentDTO> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }
    }
}
