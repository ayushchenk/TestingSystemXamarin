using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.XamarinForms.Models;

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
        }

        public ParticipateViewModel Get(int id)
        {
            string result = client.GetStringAsync(url + "/" + id).Result;
            return JsonConvert.DeserializeObject<ParticipateViewModel>(result);
        }
    }
}
