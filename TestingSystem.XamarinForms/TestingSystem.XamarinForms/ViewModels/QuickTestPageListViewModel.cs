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
    public class QuickTestPageListViewModel : INotifyPropertyChanged
    {
        private Timer timer;
        private TimeSpan time;
        private ParticipateViewModel participateModel;
        private ICommand finishCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TimeString
        {
             get
             {
                 Notify();
                 return time.Hours > 0 ? time.ToString(@"hh\:mm\:ss") : time.ToString(@"mm\:ss");
             }
        }

        public DataTemplateSelector ItemTemplate { set; get; }
        public ObservableCollection<QuestionAnswer> Model { set; get; }

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

        public QuickTestPageListViewModel(ParticipateViewModel participateModel)
        {
            if (Services.Service.HasInternetConnection())
            {
                timer = new Timer(TimeSpan.FromSeconds(1), TimerCallback);
                ItemTemplate = new QuestionDataTemplateSelectorList();
                Model = new ObservableCollection<QuestionAnswer>();

                this.participateModel = participateModel;

                foreach (var item in participateModel.QuestionAnswers)
                    Model.Add(item);

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
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ResultPageList(participateModel)) { BarBackgroundColor = Color.Gray });
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
