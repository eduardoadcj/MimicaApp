using MimicaApp.Model;
using MimicaApp.Storage;
using MimicaApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MimicaApp.ViewModel {
    public class ResultViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nameProperty) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(nameProperty));
            }
        }

        public Team WinnerTeam { get; set; }
        public Team LoserTeam { get; set; }

        public Command PlayAgain { get; set; }

        public ResultViewModel() {
            PlayAgain = new Command(OnPlayAgain);
            WinnerTeam = GameSession.Game.GetWinner();
            LoserTeam = GameSession.Game.GetLoser();
        }

        private void OnPlayAgain() {
            App.Current.MainPage = new HomePage();
        }

    }
}
