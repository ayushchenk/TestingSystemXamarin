using MonkeyCache;
using MonkeyCache.FileStore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.Infrastructure;
using TestingSystem.XamarinForms.SQLite.Repository;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms
{
    public partial class App : Application
    {
        private const string dbName = "TestingSystemXamarin.db";
        private static ParticipateRepository participateRepository;
        public static ParticipateRepository ParticipateRepository
        {
            get
            {
                if (participateRepository == null)
                    participateRepository = new ParticipateRepository(dbName);
                return participateRepository;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            Barrel.ApplicationId = "TestingSystemXamarin";
            Barrel.Current.EmptyAll();
        }

        protected override void OnStart() { Settings.ReadSettings(); }

        protected override void OnSleep() { Settings.WriteSettings(); }

        protected override void OnResume() { }
    }
}
