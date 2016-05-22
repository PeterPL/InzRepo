using System;
using System.Windows.Controls;

namespace MainApp
{
   public static class MusicUtils
    {
        public class SoundNote :IComparable<SoundNote> 
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

            public SoundNote(string nameOfNote)
            {
                
                Note = nameOfNote.Substring(0, nameOfNote.Length - 1);
                Octave = Int16.Parse(nameOfNote.Substring(nameOfNote.Length - 1));
            }

            public static int GetDistanceBetweenNotes(SoundNote note1, SoundNote note2)
            {
                string[] arrayOfNotes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
                int positionOfNote1, positionOfNote2;
                int numberOfNote1 = 0, numberOfNote2 = 0;

                for (int i = 0; i < arrayOfNotes.Length; i++)
                {
                    if (note1.Note == arrayOfNotes[i]) numberOfNote1 = i+1;
                    if (note2.Note == arrayOfNotes[i]) numberOfNote2 = i+1;
                }

                positionOfNote1 = note1.Octave*12 + numberOfNote1;
                positionOfNote2 = note2.Octave*12 + numberOfNote2;

                return Math.Abs(positionOfNote1 - positionOfNote2);
            }

            public void FindNoteByFrequency(double frequency)
            {
                var linearFrequency = Math.Log(frequency / 440.0, 2) + 4.0;
                var octave = Convert.ToInt32(Math.Floor(linearFrequency));
                var cents = Convert.ToInt32(1200 * (linearFrequency - octave));
                var noteNumber = Convert.ToInt32(cents / 100) % 12;
                cents -= noteNumber * 100;
                if (cents > 50)
                {
                    cents -= 100;
                    if (++noteNumber > 11) noteNumber -= 12;
                }
                string noteName = FindNameOfNoteByNumber(noteNumber);
                if (noteNumber > 2) octave++;
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

            public override string ToString()
            {
                return Note + Octave;
            }

            public override bool Equals(object o)
            {
                if (o == null) return false;
                var otherSoundNote = o as SoundNote;

                if (IsOutOfTune == otherSoundNote.IsOutOfTune && this.Note.Equals(otherSoundNote.Note) &&
                    Octave == otherSoundNote.Octave) return true;
                return false;
            }

            public int CompareTo(SoundNote otherSoundNote)
            {
                string[] arrayOfNotes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
                if (Octave > otherSoundNote.Octave) return 1;
                if (Octave < otherSoundNote.Octave) return -1;
                int note1 = 0, note2 = 0;
                for (var i = 0; i < arrayOfNotes.Length; i++)
                {
                    if (Note.Equals(arrayOfNotes[i])) note1 = i;
                    if (otherSoundNote.Note.Equals(arrayOfNotes[i])) note2 = i;
                }
                if (note1 < note2) return -1;
                if (note1 > note2) return 1;
                return 0;
            }

        }
    }
}
