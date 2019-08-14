using MonkeyCache;
using MonkeyCache.FileStore;
using System;
using System.Collections;
using System.Collections.Generic;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.Infrastructure;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms
{
    public partial class App : Application
    {
        private CacheProvider cacheProvider;

        public App()
        {
            InitializeComponent();

            cacheProvider = new CacheProvider();
            MainPage = new NavigationPage(new LoginPage());
            Barrel.ApplicationId = "TestingSystemXamarin";
            Barrel.Current.EmptyAll();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
