using MimicaApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MimicaApp.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage {
        public GamePage() {
            InitializeComponent();
            BindingContext = new GameViewModel();
        }
    }
}