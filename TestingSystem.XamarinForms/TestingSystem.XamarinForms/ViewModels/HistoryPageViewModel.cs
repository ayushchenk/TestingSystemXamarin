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
        private string email;
        private ResultService resultService;
        private StudentService studentService;

        public IEnumerable<StudentTestResultDTO> Results { set; get; }

        public HistoryPageViewModel(string email)
        {
            this.email = email;
            resultService = new ResultService();
            studentService = new StudentService();
            var student = studentService.Get(email);
            if (student != null)
                Results = resultService.GetAll().Where(result => result.StudentId == student.Id);
            else
                Results = null;
        }
    }
}
