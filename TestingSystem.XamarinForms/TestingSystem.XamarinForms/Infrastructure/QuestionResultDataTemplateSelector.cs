using System;
using System.Collections.Generic;
using System.Text;
using TestingSystem.XamarinForms.Controls;
using TestingSystem.XamarinForms.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TestingSystem.XamarinForms.Infrastructure
{
    public class QuestionResultDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate oneOneTemplate = new DataTemplate(typeof(OneAnswerOneCorrectResultViewCell));
        public DataTemplate manyOneTemplate = new DataTemplate(typeof(ManyAnswersOneCorrectResultViewCell));
        public DataTemplate manyManyTemplate = new DataTemplate(typeof(ManyAnswersManyCorrectResultViewCell));

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
