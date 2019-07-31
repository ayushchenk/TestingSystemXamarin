using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.ViewModels;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private string email;

        public MainPage(string email)
        {
            InitializeComponent();
            Detail = new NavigationPage(new TestPage(this.email))
            {
                BarBackgroundColor = Color.Gray,
                BarTextColor = Color.White
            };
            this.email = email;
            
            btnGroup.Clicked += (s, e) => Detail = new NavigationPage(new GroupPage(this.email)) { BarBackgroundColor = Color.Gray};
            btnTests.Clicked += (s, e) => Detail = new NavigationPage(new TestPage(this.email)) { BarBackgroundColor = Color.Gray };
            btnHistory.Clicked += (s, e) => Detail = new NavigationPage(new HistoryPage(this.email)) { BarBackgroundColor = Color.Gray };
            btnProfile.Clicked += (s, e) => Detail = new NavigationPage(new ProfilePage(this.email)) { BarBackgroundColor = Color.Gray };
        }
    }
}
