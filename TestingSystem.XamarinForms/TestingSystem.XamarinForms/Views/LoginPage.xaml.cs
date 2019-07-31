using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IEnumerable<StudentDTO> students;
        private StudentService service = new StudentService();

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;

            btnLogin.Clicked += async (s, e) =>
            {
                this.indicator.IsRunning = true;
                if (students == null)
                    students = await service.GetAllAsync();
                if (students.Select(student => student.Email).Contains(this.entryLogin.Text))
                    await Navigation.PushAsync(new MainPage(this.entryLogin.Text));
                else
                    await DisplayAlert("Alert", "Wrong credentials", "OK");
                this.indicator.IsRunning = false;
            };
        }
    }
}