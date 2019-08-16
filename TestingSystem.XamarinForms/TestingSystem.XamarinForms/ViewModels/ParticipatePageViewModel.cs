using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private TimeSpan time;
        private CacheProvider cacheProvider;
        private ResultService resultService;
        private ParticipateViewModel participateModel;
        private ICommand finishCommand;
        private ICommand previousCommand;
        private ICommand nextCommand;
        private ICommand tapCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Total { set; get; }
        public DataTemplate ItemTemplate { set; get; }
        public ObservableCollection<QuestionAnswer> Model { set; get; }

        public string TimeString
        {
            get { Notify(); return String.Format("{0:g}", time); }
        }

        public int Current
        {
            get { Notify(); return index + 1; }
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
                    nextCommand = new RelayCommand((obj) =>
                    {
                        if (index < participateModel.QuestionAnswers.Count - 1)
                        {
                            Task.Run(() =>
                            {
                                if (Model[0].QuestionType == QuestionType.ManyAnswersOneCorrect && Model[0].SelectedItem != null)
                                    Model[0].Answers.Find(a => Model[0].SelectedItem.Id == a.Id).IsPicked = true;
                                participateModel.QuestionAnswers.RemoveAt(index);
                                participateModel.QuestionAnswers.Insert(index, Model[0]);
                                Model.Clear();
                                Model.Add(participateModel.QuestionAnswers[++index]);
                                cacheProvider.Set("Participate", participateModel);
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
                    previousCommand = new RelayCommand((obj) =>
                   {
                       if (index > 0)
                       {
                           Task.Run(() =>
                           {
                               if (Model[0].QuestionType == QuestionType.ManyAnswersOneCorrect && Model[0].SelectedItem != null)
                                   Model[0].Answers.Find(a => Model[0].SelectedItem.Id == a.Id).IsPicked = true;
                               participateModel.QuestionAnswers.RemoveAt(index);
                               participateModel.QuestionAnswers.Insert(index, Model[0]);
                               Model.Clear();
                               Model.Add(participateModel.QuestionAnswers[--index]);
                               cacheProvider.Set("Participate", participateModel);
                           });
                       }
                   });
                return previousCommand;
            }
        }

        public ParticipatePageViewModel(ParticipateViewModel participateModel)
        {
            if (Services.Service.HasInternetConnection())
            {
                ItemTemplate = new QuestionDataTemplateSelector();
                this.index = 0;

                cacheProvider = new CacheProvider();
                resultService = new ResultService();

                this.participateModel = participateModel;
                time = participateModel.StartTime.AddMinutes(participateModel.Length) - DateTime.Now;

                Total = participateModel.QuestionAnswers.Count;
                Model = new ObservableCollection<QuestionAnswer>();
                Model.Add(participateModel.QuestionAnswers[index]);

                Device.StartTimer(TimeSpan.FromSeconds(1), TimerCallback);
            }
        }

        private void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async void Finish(object obj = null)
        {
            if (Services.Service.HasInternetConnection())
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoadingPopup());
                var result = new StudentTestResultDTO()
                {
                    StudentId = participateModel.StudentId,
                    GroupInTestId = participateModel.GroupInTestId
                };
                foreach (var qa in participateModel.QuestionAnswers)
                {
                    switch (qa.QuestionType)
                    {
                        case QuestionType.OneAnswerOneCorrect:
                            if (!String.IsNullOrWhiteSpace(qa.AnswerString) && qa.AnswerString.Trim().ToLower() == qa.Answers[0].AnswerString.Trim().ToLower())
                                result.Result++;
                            break;
                        case QuestionType.ManyAnswersOneCorrect:
                            if (qa.SelectedItem != null && qa.SelectedItem.IsCorrect)
                                result.Result++;
                            break;
                        case QuestionType.ManyAnswersManyCorrect:
                            if (qa.Answers.Where(a => a.IsCorrect).Select(a => a.Id).SequenceEqual(qa.Answers.Where(a => a.IsPicked).Select(a => a.Id)))
                                result.Result++;
                            break;
                    }
                }
                participateModel.Result = result.Result;

                await resultService.PostAsync(result);
                await cacheProvider.RemoveAsync("Participate");
                await cacheProvider.RemoveAsync("Tests");
                await cacheProvider.RemoveAsync("History");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ResultPage(participateModel)));
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
        }

        private bool TimerCallback()
        {
            time = time.Subtract(TimeSpan.FromSeconds(1));
            if (time > TimeSpan.FromSeconds(1))
                return true;
            else
            {
                Finish(null);
                return false;
            }
        }
    }
}
