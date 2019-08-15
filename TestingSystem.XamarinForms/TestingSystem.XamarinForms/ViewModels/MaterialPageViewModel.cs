using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Infrastructure;
using TestingSystem.XamarinForms.Models;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class MaterialPageViewModel : INotifyPropertyChanged
    {
        private StudentDTO student;
        private IEnumerable<StudyingMaterialDTO> studyingMaterials;

        private ICommand itemTappedCommand;
        private ICommand refreshCommand;
        private CacheProvider cacheProvider;
        private StudentService studentService;
        private StudyingMaterialService materialService;

        public event PropertyChangedEventHandler PropertyChanged;
        public StudyingMaterialDTO SelectedItem { set; get; }

        public IEnumerable<StudyingMaterialDTO> StudyingMaterials
        {
            set
            {
                studyingMaterials = value;
                Notify();
            }
            get { return studyingMaterials; }

        }

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                    refreshCommand = new RelayCommand(async (obj) =>
                   {
                       StudyingMaterials = (await materialService.GetAllAsync()).Where(material => material.SpecializationId == student.SpecializationId);
                       await Task.Run(() => cacheProvider.Set("StudyingMaterials", StudyingMaterials.ToList()));
                   });
                return refreshCommand;
            }
        }

        public ICommand ItemTappedCommand
        {
            get
            {
                if (itemTappedCommand == null)
                    itemTappedCommand = new RelayCommand(obj => Device.OpenUri(new Uri(SelectedItem.Link)));
                return itemTappedCommand;
            }
        }

        public MaterialPageViewModel(int id)
        {
            if (Services.Service.HasInternetConnection())
            {
                Task.Run(async () =>
                {
                    cacheProvider = new CacheProvider();
                    studentService = new StudentService();
                    materialService = new StudyingMaterialService();
                    student = cacheProvider.Get<StudentDTO>("Student") ?? await studentService.GetAsync(id);
                    StudyingMaterials = cacheProvider.Get<List<StudyingMaterialDTO>>("StudyingMaterials")
                        ?? (await materialService.GetAllAsync()).Where(material => material.SpecializationId == student.SpecializationId);
                    cacheProvider.Set("Student", student);
                    cacheProvider.Set("StudyingMaterials", StudyingMaterials.ToList());
                });

            }
        }

        private void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
