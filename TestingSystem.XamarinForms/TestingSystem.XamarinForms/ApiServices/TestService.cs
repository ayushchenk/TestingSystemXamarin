﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestingSystem.BusinessModel.Model;

namespace TestingSystem.XamarinForms.ApiServices
{
    public class TestService
    {
        const string url = "https://testingsystemapplication.azurewebsites.net/api/testapi";
        private HttpClient client;

        public TestService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public IEnumerable<GroupsInTestDTO> GetAll()
        {
            string result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<GroupsInTestDTO>>(result).Where(git => DateTime.Now <= git.StartTime.Value.AddMinutes(git.Length));
        }

        public Task<IEnumerable<GroupsInTestDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }
    }
}