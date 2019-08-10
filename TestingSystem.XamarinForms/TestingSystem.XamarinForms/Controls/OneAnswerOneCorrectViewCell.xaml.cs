using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneAnswerOneCorrectViewCell : ViewCell
    {
        public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(nameof(ImagePath), typeof(string), typeof(OneAnswerOneCorrectViewCell), default(string), BindingMode.TwoWay);
        public string ImagePath
        {
            get
            {
                return (string)GetValue(ImagePathProperty);
            }

            set
            {
                SetValue(ImagePathProperty, value);
            }
        }

        public static readonly BindableProperty QuestionStringProperty = BindableProperty.Create(nameof(QuestionString), typeof(string), typeof(OneAnswerOneCorrectViewCell), default(string), BindingMode.TwoWay);
        public string QuestionString
        {
            get
            {
                return (string)GetValue(QuestionStringProperty);
            }

            set
            {
                SetValue(QuestionStringProperty, value);
            }
        }

        public static readonly BindableProperty AnswerStringProperty = BindableProperty.Create(nameof(AnswerString), typeof(string), typeof(OneAnswerOneCorrectViewCell), default(string), BindingMode.TwoWay);
        public string AnswerString
        {
            get
            {
                return (string)GetValue(AnswerStringProperty);
            }

            set
            {
                SetValue(AnswerStringProperty, value);
            }
        }

        public OneAnswerOneCorrectViewCell()
        {
            InitializeComponent();
        }
    }
}