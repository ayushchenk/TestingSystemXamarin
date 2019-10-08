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
    public class ParticipatePageListViewModel : INotifyPropertyChanged
    {
        private bool isCachingStopped;
        private Timer timer;
        private Timer cacheTimer;
        private TimeSpan time;
        private CacheProvider cacheProvider;
        private ResultService resultService;
        private ParticipateViewModel participateModel;
        private ICommand finishCommand;
        private ICommand scrolledCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Total { set; get; }
        public DataTemplate ItemTemplate { set; get; }
        public ObservableCollection<QuestionAnswer> Model { set; get; }

        public string TimeString
        {
            get
            {
                Notify();
                return time.Hours > 0 ? time.ToString(@"hh\:mm\:ss") : time.ToString(@"mm\:ss");
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

        public ICommand ScrolledCommand
        {
            get
            {
                if (scrolledCommand == null)
                    scrolledCommand = new RelayCommand(async (obj) =>
                    {
                        if(!isCachingStopped)
                            await cacheProvider.SetAsync("Participate" + participateModel.GroupInTestId, participateModel);
                        isCachingStopped = true;
                        cacheTimer.Start();
                    });
                return scrolledCommand;
            }
        }

        public ParticipatePageListViewModel(ParticipateViewModel participateModel)
        {
            if (Services.Service.HasInternetConnection())
            {
                ItemTemplate = new QuestionDataTemplateSelectorList();
                timer = new Timer(TimeSpan.FromSeconds(1), TimerCallback);
                cacheTimer = new Timer(TimeSpan.FromSeconds(5), () =>
                {
                    isCachingStopped = false;
                    cacheTimer.Stop();
                    return isCachingStopped;
                });

                cacheProvider = new CacheProvider();
                resultService = new ResultService();

                this.participateModel = participateModel;
                time = participateModel.StartTime.AddMinutes(participateModel.Length) - DateTime.Now;

                Total = participateModel.QuestionAnswers.Count;
                Model = new ObservableCollection<QuestionAnswer>();

                foreach (var item in participateModel.QuestionAnswers)
                    Model.Add(item);

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
                var model = await App.ParticipateRepository.SaveAsync(new SQLite.Model.ParticipateSQLiteModel
                {
                    ParticipateModelJSONString = Newtonsoft.Json.JsonConvert.SerializeObject(participateModel),
                    GroupInTestId = participateModel.GroupInTestId
                });
                await resultService.PostAsync(result);
                await cacheProvider.RemoveAsync("Participate" + participateModel.GroupInTestId);
                await cacheProvider.RemoveAsync("Tests");
                await cacheProvider.RemoveAsync("History");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ResultPageList(participateModel)));
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
                Finish();
                return false;
            }
        }
    }
}
