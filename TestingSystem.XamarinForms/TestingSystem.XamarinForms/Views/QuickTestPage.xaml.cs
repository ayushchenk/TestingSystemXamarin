﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.XamarinForms.Models;
using TestingSystem.XamarinForms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingSystem.XamarinForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickTestPage : ContentPage
    {
        public QuickTestPage(ParticipateViewModel participateModel)
        {
            InitializeComponent();
            BindingContext = new QuickTestPageViewModel(participateModel);
        }
    }
}