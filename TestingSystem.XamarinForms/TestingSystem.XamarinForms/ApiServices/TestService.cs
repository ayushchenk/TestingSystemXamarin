﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestingSystem.BusinessModel.Model;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ApiServices
{
    public class TestService
    {
        const string url = "https://testingsystemapplication.azurewebsites.net/api/testapi";
        private HttpClient client;
        private ResultService resultService;

        public TestService()
        {
            resultService = new ResultService();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Services.Service.SetBasicAuthHeader(client);
        }

        public IEnumerable<GroupsInTestDTO> GetAll()
        {
            try
            {
                string result = client.GetStringAsync(url).Result;
                var results = resultService.GetAll();
                return JsonConvert.DeserializeObject<IEnumerable<GroupsInTestDTO>>(result).Where(git => !results.Select(r => r.GroupInTestId).Contains(git.Id) && DateTime.Now <= git.StartTime.Value.AddMinutes(git.Length));
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Check interner connection", $"An error occured while processing web request", "OK");
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            return null;
        }

        public Task<IEnumerable<GroupsInTestDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }
    }
}
