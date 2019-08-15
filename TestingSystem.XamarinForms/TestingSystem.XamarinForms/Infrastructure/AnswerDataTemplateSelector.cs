using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TestingSystem.BusinessModel.Model;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.Infrastructure
{
    class AnswerDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CorrectAnswer { get; set; }
        public DataTemplate WrongAnswer { get; set; }
        public DataTemplate Answer { set; get; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            QuestionAnswerDTO value = item as QuestionAnswerDTO;
            if (value.IsCorrect)
                return CorrectAnswer;
            if (value.IsPicked)
                return WrongAnswer;
            return Answer;
        }
    }
}
