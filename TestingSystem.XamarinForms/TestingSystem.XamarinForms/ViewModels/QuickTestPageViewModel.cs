using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestingSystem.XamarinForms.ApiServices;
using TestingSystem.XamarinForms.Infrastructure;
using TestingSystem.XamarinForms.Models;
using TestingSystem.XamarinForms.Views;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.ViewModels
{
    public class QuickTestPageViewModel : INotifyPropertyChanged
    {
        private int index;
        private Timer timer;
        private TimeSpan time;
        private ParticipateViewModel participateModel;
        private QuickTestService testService;
        private ICommand finishCommand;
        private ICommand previousCommand;
        private ICommand nextCommand;
        private ICommand tapCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TimeString
        {
             get
             {
                 Notify();
                 return time.Hours > 0 ? time.ToString(@"hh\:mm\:ss") : time.ToString(@"mm\:ss");
             }
        }
        public int Total { set; get; }
        public DataTemplate ItemTemplate { set; get; }
        public ObservableCollection<QuestionAnswer> Model { set; get; }

        public int Current
        {
            get { Notify(); return index + 1; }
        }

        public ICommand FinishCommand
        {
            get
            {
                if (finishCommand == null)
                    finishCommand = new RelayCommand(async (obj) =>
                    {
                        if (await Application.Current.MainPage.DisplayAlert("Finish", "Are you sure?", "Yes", "No"))
                            Finish();
                    });
                return finishCommand;
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                    nextCommand = new RelayCommand(async (obj) =>
                    {
                        if (index < participateModel.QuestionAnswers.Count - 1)
                        {
                            await Task.Run(() =>
                            {
                                if (Model[0].QuestionType == QuestionType.ManyAnswersOneCorrect && Model[0].SelectedItem != null)
                                    Model[0].Answers.Find(a => Model[0].SelectedItem.Id == a.Id).IsPicked = true;
                                participateModel.QuestionAnswers.RemoveAt(index);
                                participateModel.QuestionAnswers.Insert(index, Model[0]);
                                Model.Clear();
                                Model.Add(participateModel.QuestionAnswers[++index]);
                            });
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
                    previousCommand = new RelayCommand(async (obj) =>
                    {
                        if (index > 0)
                        {
                            await Task.Run(() =>
                            {
                                if (Model[0].QuestionType == QuestionType.ManyAnswersOneCorrect && Model[0].SelectedItem != null)
                                    Model[0].Answers.Find(a => Model[0].SelectedItem.Id == a.Id).IsPicked = true;
                                participateModel.QuestionAnswers.RemoveAt(index);
                                participateModel.QuestionAnswers.Insert(index, Model[0]);
                                Model.Clear();
                                Model.Add(participateModel.QuestionAnswers[--index]);
                            });
                        }
                    });
                return previousCommand;
            }
        }

        public ICommand ImageTapCommand
        {
            get
            {
                if (tapCommand == null)
                    tapCommand = new RelayCommand(async (source) =>
                    {
                        if (!String.IsNullOrWhiteSpace(Model[0].Question.ImagePath))
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ImagePopup(Model[0].Question.ImagePath));
                    });
                return tapCommand;
            }
        }

        public QuickTestPageViewModel(ParticipateViewModel participateModel)
        {
            if (Services.Service.HasInternetConnection())
            {
                index = 0;
                timer = new Timer(TimeSpan.FromSeconds(1), TimerCallback);
                testService = new QuickTestService();
                ItemTemplate = new QuestionDataTemplateSelector();
                Model = new ObservableCollection<QuestionAnswer>();

                this.participateModel = participateModel;

                Total = participateModel.QuestionAnswers.Count;
                Model.Add(participateModel.QuestionAnswers[index]);

                time = TimeSpan.FromMinutes(participateModel.Length);
                timer.Start();
            }
        }

        private void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async void Finish(object obj = null)
        {
            timer.Stop();
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoadingPopup());
            foreach (var qa in participateModel.QuestionAnswers)
            {
                switch (qa.QuestionType)
                {
                    case QuestionType.OneAnswerOneCorrect:
                        if (!String.IsNullOrWhiteSpace(qa.AnswerString) && qa.AnswerString.Trim().ToLower() == qa.Answers[0].AnswerString.Trim().ToLower())
                            participateModel.Result++;
                        break;
                    case QuestionType.ManyAnswersOneCorrect:
                        if (qa.SelectedItem != null && qa.SelectedItem.IsCorrect)
                            participateModel.Result++;
                        break;
                    case QuestionType.ManyAnswersManyCorrect:
                        if (qa.Answers.Where(a => a.IsCorrect).Select(a => a.Id).SequenceEqual(qa.Answers.Where(a => a.IsPicked).Select(a => a.Id)))
                            participateModel.Result++;
                        break;
                }
            }
            await Application.Current.MainPage.Navigation.PopToRootAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ResultPage(participateModel)) { BarBackgroundColor = Color.Gray });
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private bool TimerCallback()
        {
            time = time.Subtract(TimeSpan.FromSeconds(1));
            if (time > TimeSpan.FromSeconds(1))
                return true;
            else
            {
                Finish();
                return false;
            }
        }
    }
}
