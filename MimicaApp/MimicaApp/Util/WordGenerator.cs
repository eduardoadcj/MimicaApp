using MimicaApp.Storage;
using MimicaApp.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MimicaApp.Util {
    public class WordGenerator {

        public static byte[] Levels = {1, 3, 5};

        public static string[][] Words = {
            //Easy
            new string[] {"Eye", "Tongue", "Shoes", "Corn", "Ball", "Ping-pong"},

            //Medium
            new string[] {"Carpenter", "Yellow", "Lime", "Bee", "Lamp"},

            //Hard
            new string[] {"Flashlight", "Batman", "Notebook"}
        };

        public GenWord GetWord(string lvl) {
            
            int lvlIndex = 0;

            if (lvl.Equals("Easy")) {
                lvlIndex = 0;
            } else if (lvl.Equals("Medium")) {
                lvlIndex = 1;
            } else if (lvl.Equals("Hard")) {
                lvlIndex = 2;
            } else {
                return GetRandomWord();
            }

            Random ran = new Random();
            int index = ran.Next(0, Words[lvlIndex].Length - 1);

            GenWord gw = new GenWord();
            gw.Word = Words[lvlIndex][index];
            gw.Points = Levels[lvlIndex];

            return gw;
        }

        public GenWord GetRandomWord() {
            Random ran = new Random();
            int lvlIndex = ran.Next(0, 2);
            int index = ran.Next(0, Words[lvlIndex].Length - 1);
            GenWord gw = new GenWord();
            gw.Word = Words[lvlIndex][index];
            gw.Points = Levels[lvlIndex];
            return gw;
        }

    }
}
