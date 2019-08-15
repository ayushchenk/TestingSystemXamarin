using System.Collections.Generic;
using System.Linq;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using System.ComponentModel;
using TestingSystem.XamarinForms.Infrastructure;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TestingSystem.XamarinForms.Models;
using System.Collections.ObjectModel;

namespace TestingSystem.XamarinForms.ViewModels
{
    class HistoryPageViewModel : INotifyPropertyChanged
    {
        private StudentDTO student;
        private IEnumerable<StudentTestResultDTO> results;
        private CacheProvider cacheProvider;
        private ResultService resultService;
        private StudentService studentService;
        private ICommand refreshCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                    refreshCommand = new RelayCommand(async (obj) =>
                    {
                        Results = ( await resultService.GetAllAsync()).Where(result => result.StudentId == student.Id);
                        await Task.Run(() => cacheProvider.Set("Results", Results.ToList()));
                    });
                return refreshCommand;
            }
        }


        public IEnumerable<StudentTestResultDTO> Results
        {
            set
            {
                results = value;
                Notify();
            }
            get { return results; }
        }

        public HistoryPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                Task.Run(async () =>
                {
                    resultService = new ResultService();
                    studentService = new StudentService();
                    cacheProvider = new CacheProvider();

                    student = cacheProvider.Get<StudentDTO>("Student") ?? await studentService.GetAsync(id);
                    if (student != null)
                    {
                        Results = cacheProvider.Get<List<StudentTestResultDTO>>("Results")
                            ?? (await resultService.GetAllAsync()).Where(result => result.StudentId == student.Id);
                        cacheProvider.Set("Results", Results.ToList());
                    }
                    else
                        Results = null;
                });
            }
        }

        private void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
