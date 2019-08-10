using System;
using System.Collections.Generic;
using System.Text;
using TestingSystem.XamarinForms.Controls;
using TestingSystem.XamarinForms.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TestingSystem.XamarinForms.Infrastructure
{
    public class QuestionDataTemplateSelector:DataTemplateSelector
    {
        private DataTemplate oneOneTemplate = new DataTemplate(typeof(OneAnswerOneCorrectViewCell));
        private DataTemplate manyOneTemplate = new DataTemplate(typeof(ManyAnswersOneCorrectViewCell));
        private DataTemplate manyManyTemplate = new DataTemplate(typeof(ManyAnswersManyCorrectViewCell));

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
