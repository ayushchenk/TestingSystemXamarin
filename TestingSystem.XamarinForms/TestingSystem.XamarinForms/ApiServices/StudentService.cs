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
    public class StudentService
    {
        const string url = "https://testingsystemapplication.azurewebsites.net/api/studentapi";
        private HttpClient client;

        public StudentService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public IEnumerable<StudentDTO> GetAll()
        {
            string result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<StudentDTO>>(result);
        }

        public Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public StudentDTO Get(int id)
        {
            string result = client.GetStringAsync(url + "/" + id).Result;
            return JsonConvert.DeserializeObject<StudentDTO>(result);
        }
    }
}
