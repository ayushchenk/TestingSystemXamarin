using System;
using System.Collections.Generic;
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
        private TestService testService;
        private StudentService studentService;
        private ICommand itemTappedCommand;
        public IEnumerable<GroupsInTestDTO> Tests { set; get; }
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

        public TestPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                this.id = id;
                testService = new TestService();
                studentService = new StudentService();
                var student = studentService.Get(id);
                Tests = testService.GetAll().Where(git => git.GroupId == student.GroupId);
            }
        }
    }
}
