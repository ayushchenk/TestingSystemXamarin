using System;
using System.Collections.Generic;
using System.Text;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestingSystem.XamarinForms.Infrastructure;
using System.Threading.Tasks;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        private StudentService service;
        private StudentDTO student;
        private CacheProvider cacheProvider;

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

        public ProfilePageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                Task.Run(async () =>
                {
                    service = new StudentService();
                    cacheProvider = new CacheProvider();
                    Student = cacheProvider.Get<StudentDTO>("Student") ?? await service.GetAsync(id);
                    cacheProvider.Set("Student", Student);
                });
            }
        }

        private void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
