using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Models;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class QuickTestSetupPageViewModel : INotifyPropertyChanged
    {
        private QuickTestService testService;
        private SubjectService subjectService;
        private SpecializationService specializationService;
        private IEnumerable<SubjectDTO> allSubjects;
        private IEnumerable<SpecializationDTO> specializations;
        private ICommand nextCommand;
        private ICommand itemChangedCommand;

        public string QuestionCount { set; get; }
        public SubjectDTO SelectedSubject { set; get; }
        public SpecializationDTO SelectedSpecialization { set; get; }

        public ObservableCollection<SubjectDTO> Subjects { set; get; }

        public IEnumerable<SpecializationDTO> Specializations
        {
            set
            {
                specializations = value;
                Notify();
            }
            get { return specializations; }
        }

        public ICommand ItemChangedCommand
        {
            get
            {
                if (itemChangedCommand == null)
                    itemChangedCommand = new RelayCommand(obj =>
                    {
                        Subjects.Clear();
                        if (SelectedSpecialization == null)
                            return;
                        foreach (var item in allSubjects.Where(s => s.SpecializationId == SelectedSpecialization.Id))
                            Subjects.Add(item);
                    });
                return itemChangedCommand;
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                    nextCommand = new RelayCommand(async (obj) =>
                    {
                        if (SelectedSubject == null || String.IsNullOrWhiteSpace(QuestionCount))
                        {
                            await Application.Current.MainPage.DisplayAlert("Warning", "You must select all fields", "OK");
                            return;
                        }
                        QuickTestApiModel model = new QuickTestApiModel()
                        {
                            SubjectId = SelectedSubject.Id,
                            QuestionCount = int.Parse(this.QuestionCount)
                        };

                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoadingPopup());
                        var participateModel = await testService.GetAsync(model);

                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                        await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new QuickTestPage(participateModel)));
                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                    });
                return nextCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public QuickTestSetupPageViewModel()
        {
            if (Services.Service.HasInternetConnection())
            {
                Task.Run(() =>
                {
                    Subjects = new ObservableCollection<SubjectDTO>();
                    subjectService = new SubjectService();
                    testService = new QuickTestService();
                    specializationService = new SpecializationService();
                    allSubjects = subjectService.GetAll();
                    Specializations = specializationService.GetAll();
                });
            }
        }

        private void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
