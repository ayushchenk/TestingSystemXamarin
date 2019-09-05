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
using Xamarin.Forms;
using TestingSystem.XamarinForms.Views;

namespace TestingSystem.XamarinForms.ViewModels
{
    class HistoryPageViewModel : INotifyPropertyChanged
    {
        private bool isRefreshing;
        private bool isVisible;
        private StudentDTO student;
        private IEnumerable<StudentTestResultDTO> results;
        private CacheProvider cacheProvider;
        private ResultService resultService;
        private StudentService studentService;
        private ICommand refreshCommand;
        private ICommand tapCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentTestResultDTO SelectedItem { set; get; }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                Notify();
            }
        }

        public bool IsLabelVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                Notify();
            }
        }

        public ICommand ItemTappedCommand
        {
            get
            {
                if (tapCommand == null)
                    tapCommand = new RelayCommand(async (obj) =>
                    {
                        if (SelectedItem != null)
                        {
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoadingPopup());
                            var sqliteModel = await App.ParticipateRepository.FindByAsync(x=> x.GroupInTestId == SelectedItem.GroupInTestId);
                            if (sqliteModel.Count != 0)
                            {
                                var participateModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParticipateViewModel>(sqliteModel.First().ParticipateModelJSONString);
                                await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ResultPage(participateModel)));
                            }
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                        }
                    });
                return tapCommand;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                    refreshCommand = new RelayCommand(async (obj) =>
                    {
                        if (Services.Service.HasInternetConnection())
                        {
                            Results = (await resultService.GetAllAsync()).Where(result => result.StudentId == student.Id);
                            IsLabelVisible = Results.Count() != 0 ? false : true;
                            await cacheProvider.SetAsync("Results", Results.ToList());
                            IsRefreshing = false;
                        }
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

                    student = await cacheProvider.GetAsync<StudentDTO>("Student") ?? await studentService.GetAsync(id);
                    if (student != null)
                    {
                        Results = await cacheProvider.GetAsync<List<StudentTestResultDTO>>("Results")
                            ?? (await resultService.GetAllAsync()).Where(result => result.StudentId == student.Id);
                        IsLabelVisible = Results.Count() != 0 ? false : true;
                        await cacheProvider.SetAsync("Results", Results.ToList());
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
