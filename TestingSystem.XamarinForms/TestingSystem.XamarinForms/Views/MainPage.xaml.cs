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
        private int id;

        public MainPage(int id)
        {
            InitializeComponent();
            Detail = new NavigationPage(new TestPage(this.id)) { BarBackgroundColor = Color.Gray };
            this.id = id;

            btnTests.Clicked += (s, e) => Detail = new NavigationPage(new TestPage(this.id)) { BarBackgroundColor = Color.Gray };
            btnGroup.Clicked += (s, e) => Detail = new NavigationPage(new GroupPage(this.id)) { BarBackgroundColor = Color.Gray };
            btnHistory.Clicked += (s, e) => Detail = new NavigationPage(new HistoryPage(this.id)) { BarBackgroundColor = Color.Gray };
            btnProfile.Clicked += (s, e) => Detail = new NavigationPage(new ProfilePage(this.id)) { BarBackgroundColor = Color.Gray };
            btnMaterials.Clicked += (s, e) => Detail = new NavigationPage(new MaterialPage(this.id)) { BarBackgroundColor = Color.Gray };
        }
    }
}
