using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;

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
        }

        public IEnumerable<StudyingMaterialDTO> GetAll()
        {
            string result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<StudyingMaterialDTO>>(result);
        }

        public Task<IEnumerable<StudyingMaterialDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }
    }
}
