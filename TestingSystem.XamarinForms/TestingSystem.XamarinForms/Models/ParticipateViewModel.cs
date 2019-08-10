using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingSystem.BusinessModel.Model;

namespace TestingSystem.XamarinForms.Models
{
    public class ParticipateViewModel
    {
        public int GroupInTestId { set; get; }
        public int StudentId { set; get; }
        public int Length { set; get; }
        public int SubjectId { set; get; }
        public DateTime EndTime { set; get; }
        public DateTime StartTime { set; get; }
        public int QuestionCount { set; get; }
        public int Result { set; get; }
        public List<QuestionAnswer> QuestionAnswers { set; get; } = new List<QuestionAnswer>();
    }

    public class QuestionAnswer
    {
        public QuestionDTO Question { set; get; }
        public List<QuestionAnswerDTO> Answers { set; get; } = new List<QuestionAnswerDTO>();

        public string AnswerString { set; get; }
        public int AnswerId { set; get; }
        public QuestionAnswerDTO SelectedItem { set; get; }

        public QuestionType QuestionType
        {
            get
            {
                int allCount = Answers.Count();
                int correctCount = Answers.Count(answer => answer.IsCorrect);
                if (allCount > 1 && correctCount > 1) return QuestionType.ManyAnswersManyCorrect;
                if (allCount > 1 && correctCount == 1) return QuestionType.ManyAnswersOneCorrect;
                if (allCount == 1 && correctCount == 1) return QuestionType.OneAnswerOneCorrect;
                return QuestionType.Undefined;
            }
        }
    }

    public enum QuestionType
    {
        ManyAnswersOneCorrect,
        ManyAnswersManyCorrect,
        OneAnswerOneCorrect,
        Undefined
    }
}