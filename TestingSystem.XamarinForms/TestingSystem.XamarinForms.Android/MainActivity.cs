using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;

namespace TestingSystem.XamarinForms.Droid
{
    [Activity(Label = "International Education Systems", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Stormlion.PhotoBrowser.Droid.Platform.Init(this);
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            //if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            //{
            //    if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count() != 0)
            //        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            //}
        }
    }
}