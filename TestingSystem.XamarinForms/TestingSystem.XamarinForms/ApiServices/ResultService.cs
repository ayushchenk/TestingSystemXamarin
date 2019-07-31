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
    public class ResultService
    {
        private const string Url = "https://testingsystemapplication.azurewebsites.net/api/resultapi";
        private HttpClient client;

        public ResultService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public IEnumerable<StudentTestResultDTO> GetAll()
        {
            string result = client.GetStringAsync(Url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<StudentTestResultDTO>>(result);
        }

        public Task<IEnumerable<StudentTestResultDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }
    }
}
