using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginService service;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
            service = new LoginService();

            btnLogin.Clicked += async (s, e) =>
            {
                if (!Services.Service.HasInternetConnection())
                {
                    await DisplayAlert("Alert", "You have no connection to internet", "OK");
                    return;
                }

                this.indicator.IsRunning = true;
                //var result = await service.LoginAsync(entryEmail.Text, entryPassword.Text);
                var result = await service.LoginAsync("a.yushchenk@gmail.com", "Rhjn!234");
                if (result.IsSuccessful)
                {
                    await Navigation.PushAsync(new MainPage(result.Id));
                    Navigation.RemovePage(this);
                }
                else
                    await DisplayAlert("Alert", result.Message, "OK");
                this.indicator.IsRunning = false;
            };
        }

    }
}