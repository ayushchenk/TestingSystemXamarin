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
    public partial class LoadingPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingPopup()
        {
            InitializeComponent();
        }
    }
}