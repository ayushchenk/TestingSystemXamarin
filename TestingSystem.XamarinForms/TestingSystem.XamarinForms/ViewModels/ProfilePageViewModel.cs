using System;
using System.Collections.Generic;
using System.Text;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class ProfilePageViewModel
    {
        private StudentService service;

        public string Email { set; get; }
        public StudentDTO Student { set; get; }

        public ProfilePageViewModel() { }

        public ProfilePageViewModel(string email)
        {
            Email = email;
            service = new StudentService();
            Student = service.Get(Email);
        }

    }
}
