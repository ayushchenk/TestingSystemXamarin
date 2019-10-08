using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class ResultPageListViewModel 
    {
        public int Correct { set; get; }
        public DataTemplate ItemTemplate { set; get; }
        public QuestionAnswerDTO SelectedAnswer { set; get; }
        public ObservableCollection<QuestionAnswer> Model { set; get; }

        public ResultPageListViewModel(ParticipateViewModel model)
        {
            ItemTemplate = new QuestionResultDataTemplateSelectorList();

            Correct = model.Result;
            
            Model = new ObservableCollection<QuestionAnswer>();
            foreach (var item in model.QuestionAnswers)
                Model.Add(item);
        }
    }
}
