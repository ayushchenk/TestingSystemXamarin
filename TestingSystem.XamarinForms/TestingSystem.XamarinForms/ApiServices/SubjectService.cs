using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;

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
        }

        public IEnumerable<SubjectDTO> GetAll()
        {
            string result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(result);
        }

        public Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public SubjectDTO Get(int id)
        {
            string result = client.GetStringAsync(url + "/" + id).Result;
            return JsonConvert.DeserializeObject<SubjectDTO>(result);
        }

        public Task<SubjectDTO> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }
    }
}
