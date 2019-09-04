using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class ResultPageViewModel : INotifyPropertyChanged
    {
        private int index;
        private ParticipateViewModel participateModel;
        private ICommand previousCommand;
        private ICommand nextCommand;
        private ICommand tapCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Total { set; get; }
        public int Correct { set; get; }
        public DataTemplate ItemTemplate { set; get; }
        public QuestionAnswerDTO SelectedAnswer { set; get; }
        public ObservableCollection<QuestionAnswer> Model { set; get; }

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

        public ResultPageViewModel(ParticipateViewModel model)
        {
            ItemTemplate = new QuestionResultDataTemplateSelector();
            this.index = 0;

            participateModel = model;
            Correct = model.Result;
            
            Total = participateModel.QuestionAnswers.Count;
            Model = new ObservableCollection<QuestionAnswer>();
            Model.Add(participateModel.QuestionAnswers[index]);
        }

        private void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
