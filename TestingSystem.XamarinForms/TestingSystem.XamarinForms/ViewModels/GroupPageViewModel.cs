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
        private int id;
        private StudentService service;
        public IEnumerable<StudentDTO> Students { set; get; }
        public StudentDTO Student { set; get; }

        public GroupPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                this.id = id;
                service = new StudentService();
                Student = service.Get(id);
                Students = service.GetAll().Where(student => student.GroupId == this.Student.GroupId);
            }
        }

    }
}
