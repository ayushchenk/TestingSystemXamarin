using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Models;
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
            this.id = id;
            listView.ItemSelected += OnItemSelected;
            Detail = new NavigationPage(new TestPage(this.id)) ;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            { 
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType, this.id));
                listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
