using System;
using System.Collections.Generic;
using System.Text;

namespace MimicaApp.Model {
    public class Game {

        public Team TeamA { get; set; }
        public Team TeamB { get; set; }

        public string Mode { get; set; }
        public short WordTime { get; set; }
        public short Rounds { get; set; }

        public Team GetWinner() {
            return TeamA.Score >= TeamB.Score ? TeamA : TeamB;
        }

        public Team GetLoser() {
            return TeamA.Score <= TeamB.Score ? TeamA : TeamB;
        }

    }
}
