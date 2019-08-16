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
using TestingSystem.XamarinForms.Infrastructure;
using TestingSystem.XamarinForms.Models;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class TestPageViewModel:INotifyPropertyChanged
    {
        private int id;
        private StudentDTO student;
        private CacheProvider cacheProvider;
        private TestService testService;
        private ParticipateService participateService;
        private StudentService studentService;
        private ICommand itemTappedCommand;
        private ICommand refreshCommand;
        private IEnumerable<GroupsInTestDTO> tests;

        public GroupsInTestDTO SelectedItem { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<GroupsInTestDTO> Tests
        {
            set
            {
                tests = value;
                Notify();
            }
            get { return tests; }
        }

        public ICommand ItemTappedCommand
        {
            get
            {
                if (itemTappedCommand == null)
                    itemTappedCommand = new RelayCommand(async (obj) =>
                    {
                        if (SelectedItem != null)
                        {
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoadingPopup());
                            var participateModel = await cacheProvider.GetAsync<ParticipateViewModel>("Participate") ?? await participateService.GetAsync(SelectedItem.Id);
                            participateModel.StudentId = this.id;
                            participateModel.GroupInTestId = this.SelectedItem.Id;
                            await cacheProvider.SetAsync("Participate", participateModel);
                            await Application.Current.MainPage.Navigation.PushAsync(new ParticipatePage(participateModel));
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                        }
                    });
                return itemTappedCommand;
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
                            Tests = (await testService.GetAllAsync()).Where(git => git.GroupId == student.GroupId);
                            await cacheProvider.SetAsync("Tests", Tests.ToList());
                        }
                    });
                return refreshCommand;
            }
        }

        public TestPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                Task.Run(async() =>
                {
                    this.id = id;
                    testService = new TestService();
                    studentService = new StudentService();
                    cacheProvider = new CacheProvider();
                    participateService = new ParticipateService();
                    this.student = await cacheProvider.GetAsync<StudentDTO>("Student") ?? await studentService.GetAsync(id);
                    Tests = await cacheProvider.GetAsync<List<GroupsInTestDTO>>("Tests") 
                        ?? (await testService.GetAllAsync()).Where(git => git.GroupId == student.GroupId);
                    await cacheProvider.SetAsync("Student", student);
                    await cacheProvider.SetAsync("Tests", Tests.ToList());
                });
            }
        }

        private void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
