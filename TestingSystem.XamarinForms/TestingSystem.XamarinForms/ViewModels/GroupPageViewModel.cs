using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;

namespace TestingSystem.XamarinForms.ViewModels
{
    class GroupPageViewModel
    {
        private string email;
        private StudentService service;
        public IEnumerable<StudentDTO> Students { set; get; }
        public StudentDTO Student { set; get; }

        public GroupPageViewModel(string email)
        {
            this.email = email;
            service = new StudentService();
            if (Service.Service.HasInternetConnection())
            {
                Student = service.Get(email);
                Students = service.GetAll().Where(student => student.GroupId == this.Student.GroupId);
            }
        }

    }
}
