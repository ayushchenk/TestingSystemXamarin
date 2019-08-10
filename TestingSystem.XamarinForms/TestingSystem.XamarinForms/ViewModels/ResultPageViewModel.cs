using System;
using System.Collections.Generic;
using System.Text;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Models;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class ResultPageViewModel
    {
        private int id;
        private ResultService resultService;
        public ParticipateViewModel Model;


        public ResultPageViewModel(int id , ParticipateViewModel model)
        {
            this.id = id;
            this.Model = model;
            resultService = new ResultService();
        }
    }
}
