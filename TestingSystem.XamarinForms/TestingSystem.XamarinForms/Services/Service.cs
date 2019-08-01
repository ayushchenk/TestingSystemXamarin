﻿using Xamarin.Essentials;

namespace TestingSystem.XamarinForms.Services
{
    public static class Service
    {
        public static bool HasInternetConnection()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                return true;
            return false;
        }
    }
}