﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ImagePopup(string source)
        {
            InitializeComponent();
            this.img.Source = "https://testingsystemapplication.azurewebsites.net" + source;
            this.img.GestureRecognizers.Add(new TapGestureRecognizer((x) => Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync()));
        }

        public ImagePopup(ImageSource source)
        {
            InitializeComponent();
            this.img.Source = source;
            this.img.GestureRecognizers.Add(new TapGestureRecognizer((x) => Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync()));
        }
    }
}