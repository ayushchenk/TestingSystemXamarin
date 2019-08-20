using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManyAnswersOneCorrectViewCell : ViewCell
    {
        //public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(QuestionAnswerDTO), typeof(ManyAnswersOneCorrectViewCell), default(QuestionAnswerDTO), BindingMode.TwoWay);
        //public QuestionAnswerDTO SelectedItem
        //{
        //    get
        //    {
        //        return (QuestionAnswerDTO)GetValue(SelectedItemProperty);
        //    }

        //    set
        //    {
        //        SetValue(SelectedItemProperty, value);
        //    }
        //}

        //public static readonly BindableProperty QuestionStringProperty = BindableProperty.Create(nameof(QuestionString), typeof(string), typeof(ManyAnswersOneCorrectViewCell), default(string), BindingMode.TwoWay);
        //public string QuestionString
        //{
        //    get
        //    {
        //        return (string)GetValue(QuestionStringProperty);
        //    }

        //    set
        //    {
        //        SetValue(QuestionStringProperty, value);
        //    }
        //}

        public ManyAnswersOneCorrectViewCell()
        {
            InitializeComponent();
        }
    }
}