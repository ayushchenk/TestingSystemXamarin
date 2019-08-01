using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Models;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class MaterialPageViewModel
    {
        private ICommand itemTappedCommand;
        private StudentService studentService;
        private StudyingMaterialService materialService;

        public StudentDTO Student { set; get; }
        public IEnumerable<StudyingMaterialDTO> StudyingMaterials { set; get; }
        public StudyingMaterialDTO SelectedItem { set; get; }

        public ICommand ItemTappedCommand
        {
            get
            {
                if (itemTappedCommand == null)
                    itemTappedCommand = new RelayCommand(obj => Device.OpenUri(new Uri(SelectedItem.Link)));
                return itemTappedCommand;
            }
        }

        public MaterialPageViewModel() { }

        public MaterialPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                studentService = new StudentService();
                materialService = new StudyingMaterialService();
                Student = studentService.Get(id);
                StudyingMaterials = materialService.GetAll().Where(material => material.SpecializationId == Student.SpecializationId);  
            }
        }
    }
}
