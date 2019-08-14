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
    public partial class OneAnswerOneCorrectResultViewCell : ViewCell
    {
        public OneAnswerOneCorrectResultViewCell()
        {
            InitializeComponent();
        }
    }
}