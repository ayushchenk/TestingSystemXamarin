using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TestingSystem.BusinessModel.Model;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Infrastructure;
using TestingSystem.XamarinForms.Models;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class ParticipatePageViewModel : INotifyPropertyChanged
    {
        private int index;
        private int studentId;
        private int testId;
        private StudentDTO student;
        private ResultService resultService;
        private StudentService studentService;
        private ParticipateViewModel participateModel;
        private ParticipateService participateService;
        private ICommand finishCommand;
        private ICommand previousCommand;
        private ICommand nextCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Total { set; get; }
        public DataTemplate ItemTemplate { set; get; }
        public QuestionAnswerDTO SelectedAnswer { set; get; }
        public ObservableCollection<QuestionAnswer> Model { set; get; }
        public int Current
        {
            get {  Notify(); return index + 1; }
        }

        public ICommand FinishCommand
        {
            get
            {
                if (finishCommand == null)
                    finishCommand = new RelayCommand(async (obj) =>
                    {
                        if (await Application.Current.MainPage.DisplayAlert("Finish", "Are you sure?", "Yes", "No"))
                            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ResultPage(studentId, participateModel)) { BarBackgroundColor = Color.Gray });
                    });
                return finishCommand;
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                    nextCommand = new RelayCommand(obj =>
                    {
                        if (index < participateModel.QuestionAnswers.Count - 1)
                        {
                            participateModel.QuestionAnswers.RemoveAt(index);
                            participateModel.QuestionAnswers.Insert(index, Model[0]);
                            Model.Clear();
                            Model.Add(participateModel.QuestionAnswers[++index]);
                        }
                    });
                return nextCommand;
            }
        }

        public ICommand PreviousCommand
        {
            get
            {
                if (previousCommand == null)
                    previousCommand = new RelayCommand(obj =>
                    {
                        if (index > 0)
                        {
                            participateModel.QuestionAnswers.RemoveAt(index);
                            participateModel.QuestionAnswers.Insert(index, Model[0]);
                            Model.Clear();
                            Model.Add(participateModel.QuestionAnswers[--index]);
                        }
                    });
                return previousCommand;
            }
        }

        public ParticipatePageViewModel(int studentId, int testId)
        {
            if (Services.Service.HasInternetConnection())
            {
                ItemTemplate = new QuestionDataTemplateSelector();
                this.index = 0;
                this.studentId = studentId;
                this.testId = testId;

                resultService = new ResultService();
                studentService = new StudentService();
                participateService = new ParticipateService();

                student = studentService.Get(studentId);
                participateModel = participateService.Get(testId);

                Total = participateModel.QuestionAnswers.Count;
                Model = new ObservableCollection<QuestionAnswer>();
                Model.Add(participateModel.QuestionAnswers[index]);
            }
        }

        private void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
