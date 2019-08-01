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
        public StudentDTO Student { set; get; }

        public ProfilePageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                service = new StudentService();
                Student = service.Get(id);
            }
        }
    }
}
