using MimicaApp.Model;
using MimicaApp.Storage;
using MimicaApp.Util;
using MimicaApp.Util.Model;
using MimicaApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace MimicaApp.ViewModel {
    public class GameViewModel : INotifyPropertyChanged{

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nameProperty) {
            if(PropertyChanged != null) {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(nameProperty));
            }
        }

        private WordGenerator _WordGenerator;
        private GenWord _GenWord;

        private Team _CurrentTeam;
        public Team CurrentTeam {
            get { return _CurrentTeam; }
            set {
                _CurrentTeam = value;
                OnPropertyChanged("CurrentTeam");
                CurrentTeamName = _CurrentTeam.Name;
            }
        }

        private string _CurrentTeamName;
        public string CurrentTeamName {
            get { return _CurrentTeam.Name; }
            set {
                _CurrentTeamName = value;
                OnPropertyChanged("CurrentTeamName");
            }
        }

        private bool _Stop;

        private bool _IsReviewWordButtonVisible;
        public bool IsReviewWordButtonVisible {
            get { return _IsReviewWordButtonVisible; }
            set {
                _IsReviewWordButtonVisible = value;
                OnPropertyChanged("IsReviewWordButtonVisible");
            }
        }

        private bool _IsCountContainerVisible;
        public bool IsCountContainerVisible {
            get { return _IsCountContainerVisible; }
            set {
                _IsCountContainerVisible = value;
                OnPropertyChanged("IsCountContainerVisible");
            }
        }

        private bool _IsStartButtonVisible;
        public bool IsStartButtonVisible {
            get { return _IsStartButtonVisible; }
            set {
                _IsStartButtonVisible = value;
                OnPropertyChanged("IsStartButtonVisible");
            }
        }

        private string _Word;
        public string Word {
            get { return _Word; }
            set {
                _Word = value;
                OnPropertyChanged("Word");
            } 
        }

        private byte _Points;
        public byte Points {
            get { return _Points; }
            set {
                _Points = value;
                OnPropertyChanged("Points");
            } 
        }

        private string _TimeText;
        public string TimeText {
            get { return _TimeText; }
            set {
                _TimeText = value;
                OnPropertyChanged("TimeText");
            }
        }

        public Command ReviewWord { get; set; }
        public Command GotIt { get; set; }
        public Command Failed { get; set; }
        public Command Start { get; set; }

        public GameViewModel() {
            ReviewWord = new Command(OnReviewWord);
            GotIt = new Command(OnGotIt);
            Failed = new Command(OnFailed);
            Start = new Command(OnStart);

            _CurrentTeam = GameSession.Game.TeamA;
            _WordGenerator = new WordGenerator();

            Init();
        }

        private void Init() {
            _Stop = true;
            ChangeStatus("init");
            _GenWord = _WordGenerator.GetWord(GameSession.Game.Mode);
            Points = _GenWord.Points;
            Word = "**********";
        }

        private void OnReviewWord() {
            Word = _GenWord.Word;
            ChangeStatus("reviewed");
        }

        private void ChangeStatus(string status) {
            switch (status) {
                case "init":
                    IsReviewWordButtonVisible = true;
                    IsCountContainerVisible = false;
                    IsStartButtonVisible = false;
                    break;
                case "reviewed":
                    IsReviewWordButtonVisible = false;
                    IsCountContainerVisible = false;
                    IsStartButtonVisible = true;
                    break;
                case "counting":
                    IsReviewWordButtonVisible = false;
                    IsCountContainerVisible = true;
                    IsStartButtonVisible = false;
                    break;
            }
        }

        private void OnStart() {
            if (!_Stop) //Previnindo clicks enquanto o callback é chamado
                return;

            _Stop = false;
            int i = GameSession.Game.WordTime;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                TimeText = i.ToString();
                if (i == GameSession.Game.WordTime) {
                    ChangeStatus("counting");
                }
                i--;
                if(i < 0) {
                    TimeText = "Time Out!";
                    return false;
                }
                return !_Stop;
            });

        }

        private void OnGotIt() {
            CurrentTeam.Score += Points;
            EndTurn();
        }

        private void OnFailed() {
            EndTurn();
        }

        private void EndTurn() {
            _Stop = true;
            if(CurrentTeam == GameSession.Game.TeamA) {
                CurrentTeam = GameSession.Game.TeamB;
            } else {
                CurrentTeam = GameSession.Game.TeamA;
                GameSession.CurrentRound++;
            }

            if(GameSession.CurrentRound > GameSession.Game.Rounds) {
                App.Current.MainPage = new ResultPage();
            } else {
                Init();
            }
        }

    }
}
