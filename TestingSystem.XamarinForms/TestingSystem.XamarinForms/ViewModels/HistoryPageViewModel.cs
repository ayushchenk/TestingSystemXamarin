using System.Collections.Generic;
using System.Linq;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using System.ComponentModel;
using TestingSystem.XamarinForms.Infrastructure;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace TestingSystem.XamarinForms.ViewModels
{
    class HistoryPageViewModel:INotifyPropertyChanged
    {
        private IEnumerable<StudentTestResultDTO> results;
        private CacheProvider cacheProvider;
        private ResultService resultService;
        private StudentService studentService;

        public event PropertyChangedEventHandler PropertyChanged;

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

                    var student = cacheProvider.Get<StudentDTO>("Student") ?? await studentService.GetAsync(id);
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
