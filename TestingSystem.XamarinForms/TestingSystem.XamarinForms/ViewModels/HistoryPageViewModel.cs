using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;

namespace TestingSystem.XamarinForms.ViewModels
{
    class HistoryPageViewModel
    {
        private int id;
        private ResultService resultService;
        private StudentService studentService;

        public IEnumerable<StudentTestResultDTO> Results { set; get; }

        public HistoryPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                this.id = id;
                resultService = new ResultService();
                studentService = new StudentService();
                var student = studentService.Get(id);
                if (student != null)
                    Results = resultService.GetAll().Where(result => result.StudentId == student.Id);
                else
                    Results = null;
            }
        }
    }
}
