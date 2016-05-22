using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InzApp
{
    public static class MusicUtils
    {
        public class SoundNote
        {
            public string Note { get; set; }
            public int Octave { get; set; }
            public int Cents { get; set; }
            public bool IsOutOfTune { get; set; }

            public SoundNote()
            {
                Note = "A";
                Octave = 4;
                Cents = 0;
                IsOutOfTune = false;
            }

            public void FindNoteByFrequency(double frequency)
            {
                double linearFrequency = Math.Log(frequency / 440.0, 2) + 4.0;
                int octave = Convert.ToInt32(Math.Floor(linearFrequency));
                int cents = Convert.ToInt32(1200 * (linearFrequency - octave));
                int noteNumber = Convert.ToInt32(cents / 100) % 12;
                cents -= noteNumber * 100;
                if (cents > 50)
                {
                    cents -= 100;
                    if (++noteNumber > 11) noteNumber -= 12;
                }
                string noteName = FindNameOfNoteByNumber(noteNumber);
                if (noteNumber > 2 || noteNumber == 0) octave++;
                Octave = octave;
                Cents = cents;
                Note = noteName;
                
                if (Cents > 30 || Cents < -30) IsOutOfTune = true;
                else IsOutOfTune = false;

            }

            private string FindNameOfNoteByNumber(int noteNumber)
            {
                switch (noteNumber)
                {
                    case 0:
                        return "A";
                    case 1:
                        return "A#";
                    case 2:
                        return "B";
                    case 3:
                        return "C";
                    case 4:
                        return "C#";
                    case 5:
                        return "D";
                    case 6:
                        return "D#";
                    case 7:
                        return "E";
                    case 8:
                        return "F";
                    case 9:
                        return "F#";
                    case 10:
                        return "G";
                    case 11:
                        return "G#";
                    default:
                        return null;

                }
            }
        }


        


    }
}
