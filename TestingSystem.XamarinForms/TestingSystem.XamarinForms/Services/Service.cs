using Plugin.Connectivity;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using TestingSystem.XamarinForms.Resources;

namespace TestingSystem.XamarinForms.Services
{
    public static class Service
    {
        public static bool HasInternetConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public static void SetBasicAuthHeader(HttpClient client)
        {
            string authInfo = ApiInfo.USERNAME_VALUE + ":" + ApiInfo.PASSWORD_VALUE;
            authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + authInfo);
            client.DefaultRequestHeaders.Add(ApiInfo.API_KEY_HEADER, ApiInfo.API_KEY_VALUE);
        }
    }
}
