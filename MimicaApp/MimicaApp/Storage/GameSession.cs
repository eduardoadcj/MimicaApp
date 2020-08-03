using MimicaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MimicaApp.Storage {
    public class GameSession {
        public static Game Game { get; set; }

        public static short CurrentRound { get; set; }
    }
}
