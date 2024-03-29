﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public class QuestionAnswer:INotifyPropertyChanged
    {
        private QuestionAnswerDTO selected;

        public QuestionDTO Question { set; get; }
        public List<QuestionAnswerDTO> Answers { set; get; } = new List<QuestionAnswerDTO>();
        public string AnswerString { set; get; }
        public int AnswerId { set; get; }

        public int HeightRequest { get { return Answers.Count() * 75; } }

        public QuestionAnswerDTO SelectedItem
        {
            set
            {
                selected = value;
                Notify();
            }
            get { return selected; }
        }
        public string CorrectAnswerString
        {
            get
            {
                if(Answers[0] != null)
                    return Answers[0].AnswerString;
                return string.Empty;
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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