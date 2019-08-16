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
using System.Windows.Input;
using TestingSystem.XamarinForms.Models;

namespace TestingSystem.XamarinForms.ViewModels
{
    class GroupPageViewModel : INotifyPropertyChanged
    {
        private StudentDTO student;
        private StudentService service;
        private CacheProvider cacheProvider;
        private IEnumerable<StudentDTO> students;
        private ICommand refreshCommand;

        public string GroupName { get { return student.GroupName; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                    refreshCommand = new RelayCommand(async (obj) =>
                    {
                        if (Services.Service.HasInternetConnection())
                        {
                            Students = (await service.GetAllAsync()).Where(student => student.GroupId == this.student.GroupId);
                            await Task.Run(() => cacheProvider.Set("Students", Students.ToList()));
                        }
                    });
                return refreshCommand;
            }
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
                    student = cacheProvider.Get<StudentDTO>("Student") ?? await service.GetAsync(id);
                    Students = cacheProvider.Get<List<StudentDTO>>("Students")
                        ?? (await service.GetAllAsync()).Where(student => student.GroupId == this.student.GroupId);
                    cacheProvider.Set("Student", student);
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
