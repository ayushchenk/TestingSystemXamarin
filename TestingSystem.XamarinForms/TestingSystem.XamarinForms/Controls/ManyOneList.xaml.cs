using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManyOneList : ContentView
    {
        public ManyOneList()
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