using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Infrastructure;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestingSystem.XamarinForms.ViewModels
{
    class GroupPageViewModel:INotifyPropertyChanged
    {
        private StudentDTO student;
        private StudentService service;
        private CacheProvider cacheProvider;
        private IEnumerable<StudentDTO> students;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentDTO Student
        {
            set
            {
                student = value;
                Notify();
            }
            get { return student; }

        }
        public IEnumerable<StudentDTO> Students
        {
            set
            {
                students = value;
                Notify();
            }
            get { return students; }
        }

        public GroupPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                Task.Run(async () =>
                {
                    service = new StudentService();
                    cacheProvider = new CacheProvider();
                    Student = cacheProvider.Get<StudentDTO>("Student") ?? await service.GetAsync(id);
                    Students = cacheProvider.Get<List<StudentDTO>>("Students")
                        ?? (await service.GetAllAsync()).Where(student => student.GroupId == this.Student.GroupId);
                    cacheProvider.Set("Student", Student);
                    cacheProvider.Set("Students", Students.ToList());
                });
            }
        }

        private void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
