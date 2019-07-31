using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class MainPageViewModel
    {
        private SubjectService service;
        public IEnumerable<SubjectDTO> Subjects { set; get; }

        public MainPageViewModel()
        {
            service = new SubjectService();
            if (Service.Service.HasInternetConnection())
            {
                Subjects = service.GetAll();
            }
        }
    }
}
