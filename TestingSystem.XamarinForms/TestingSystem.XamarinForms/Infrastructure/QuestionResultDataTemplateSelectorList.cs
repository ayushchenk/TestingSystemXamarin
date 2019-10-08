using System;
using System.Collections.Generic;
using System.Text;
using TestingSystem.XamarinForms.Controls;
using TestingSystem.XamarinForms.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TestingSystem.XamarinForms.Infrastructure
{
    public class QuestionResultDataTemplateSelectorList : DataTemplateSelector
    {
        public DataTemplate oneOneTemplate = new DataTemplate(typeof(OneOneResultList));
        public DataTemplate manyOneTemplate = new DataTemplate(typeof(ManyOneResultList));
        public DataTemplate manyManyTemplate = new DataTemplate(typeof(ManyManyResultList));

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            QuestionAnswer qa = item as QuestionAnswer;
            if (qa == null)
                return null;
            switch(qa.QuestionType)
            {
                case QuestionType.ManyAnswersManyCorrect:
                    return manyManyTemplate;
                case QuestionType.ManyAnswersOneCorrect:
                    return manyOneTemplate;
                case QuestionType.OneAnswerOneCorrect:
                    return oneOneTemplate;
                default:
                    return null;
            }
        }
    }
}
