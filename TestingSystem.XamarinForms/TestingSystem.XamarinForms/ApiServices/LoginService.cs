﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestingSystem.BusinessModel.Model;

namespace TestingSystem.XamarinForms.ApiServices
{
    public class LoginService
    {
        private const string url = "https://testingsystemapplication.azurewebsites.net/api/loginapi";
        private HttpClient client;

        public LoginService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new FormUrlEncodedContent(keyValues);

            var response = await client.SendAsync(request);
            var result = new LoginResult() { IsSuccessful = response.IsSuccessStatusCode };
            if (result.IsSuccessful)
                result.Id = int.Parse(await response.Content.ReadAsStringAsync());
            else
                result.Message = await response.Content.ReadAsStringAsync();
            return result;
        }

    }
}
