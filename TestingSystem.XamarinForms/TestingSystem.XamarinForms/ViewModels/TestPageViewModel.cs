using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Models;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class TestPageViewModel
    {
        private int id;
        private StudentDTO student;
        private TestService testService;
        private StudentService studentService;
        private ICommand itemTappedCommand;
        private ICommand refreshCommand;
        public ObservableCollection<GroupsInTestDTO> Tests { set; get; }
        public GroupsInTestDTO SelectedItem { set; get; }

        public ICommand ItemTappedCommand
        {
            get
            {
                if (itemTappedCommand == null)
                    itemTappedCommand = new RelayCommand(async (obj) =>
                    {
                        if (SelectedItem != null)
                            await Application.Current.MainPage.Navigation.PushAsync(new ParticipatePage(id, SelectedItem.Id));
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
                        Tests.Clear();
                        foreach (var item in (await testService.GetAllAsync()).Where(git => git.GroupId == student.GroupId))
                            Tests.Add(item);
                    });
                return refreshCommand;
            }
        }

        public TestPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                this.id = id;
                testService = new TestService();
                studentService = new StudentService();
                Tests = new ObservableCollection<GroupsInTestDTO>();
                this.student = studentService.Get(id);
                foreach (var item in testService.GetAll().Where(git => git.GroupId == student.GroupId))
                    Tests.Add(item);
            }
        }
    }
}
