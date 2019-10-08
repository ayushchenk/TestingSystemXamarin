using Stormlion.PhotoBrowser;
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
        public ManyAnswersOneCorrectViewCell()
        {
            InitializeComponent();
        }

        void ImageTapped(object sender, EventArgs e)
        {
            var uri = img.Source.GetValue(UriImageSource.UriProperty);
            PhotoBrowser photoBrowser = new PhotoBrowser()
            {
                Photos = new List<Photo>() { new Photo { URL = uri.ToString() } },
            };
            photoBrowser.Show();
        }
    }
}