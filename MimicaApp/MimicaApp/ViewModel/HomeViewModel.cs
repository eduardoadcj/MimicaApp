using MimicaApp.Model;
using MimicaApp.Storage;
using MimicaApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MimicaApp.ViewModel {
    public class HomeViewModel : INotifyPropertyChanged{

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nameProperty) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(nameProperty));
            }
        }

        public Game Game { get; set; }

        private short _WordTime = 30;
        public short WordTime {
            get { return _WordTime; }
            set {
                _WordTime = value;
                OnPropertyChanged("WordTime");
            }
        }

        private short _Rounds = 5;
        public short Rounds {
            get { return _Rounds; }
            set {
                _Rounds = value;
                OnPropertyChanged("Rounds");
            }
        }

        public Command StartCommand { get; set; }

        public HomeViewModel() {
            Game = new Game();
            Game.TeamA = new Team();
            Game.TeamB = new Team();
            StartCommand = new Command(StartGame);
        }

        private void StartGame() {
            this.Game.WordTime = WordTime;
            this.Game.Rounds = Rounds;

            if(this.Game.TeamA.Name == null || this.Game.TeamA.Name == "") {
                this.Game.TeamA.Name = "Team A";
            }

            if(this.Game.TeamB.Name == null || this.Game.TeamB.Name == "") {
                this.Game.TeamB.Name = "Team B";
            }

            GameSession.Game = this.Game;
            GameSession.CurrentRound = 1;
            App.Current.MainPage = new GamePage();
        }

    }
}
