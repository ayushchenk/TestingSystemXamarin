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
        private SubjectService subjectService;
        private SpecializationService specializationService;
        private SpecializationDTO selectedSpecialization;
        private IEnumerable<SubjectDTO> allSubjects;
        private IEnumerable<SpecializationDTO> specializations;
        private ICommand nextCommand;
        private ICommand refreshCommand;

        public string QuestionCount { set; get; }
        public SubjectDTO SelectedSubject { set; get; }
        public ObservableCollection<SubjectDTO> Subjects { set; get; }

        public SpecializationDTO SelectedSpecialization
        {
            set
            {
                selectedSpecialization = value;
                Subjects.Clear();
                foreach (var item in allSubjects.Where(s => s.SpecializationId == value.Id))
                    Subjects.Add(item);
            }
            get { return selectedSpecialization; }
        }

        public IEnumerable<SpecializationDTO> Specializations
        {
            set
            {
                specializations = value;
                Notify();
            }
            get { return specializations; }
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
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                        await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new QuickTestPage(model)));
                    });
                return nextCommand;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                    refreshCommand = new RelayCommand((obj) => allSubjects = subjectService.GetAll());
                return refreshCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public QuickTestSetupPageViewModel()
        {
            if (Services.Service.HasInternetConnection())
            {
                Task.Run(async () =>
                {
                    subjectService = new SubjectService();
                    specializationService = new SpecializationService();
                    Subjects = new ObservableCollection<SubjectDTO>();
                    allSubjects = subjectService.GetAll();
                    Specializations = await specializationService.GetAllAsync();
                });
            }
        }

        private void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
