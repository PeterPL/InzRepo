using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Audio;

namespace MainApp
{
    public class SoundManager
    {
        private readonly Microphone _microphone = Microphone.Default;
        private readonly byte[] _buffer;
        private List<SoundNote> _notesList;
        public List<SoundNote> AchievedNotes; 
        private Analysis _page;
        public SoundNote HighestNote;
        public SoundNote LowestNote;
        private double[] a;
        public delegate void CorrectNoteHandler(Microphone mic);
        public event CorrectNoteHandler CorrectNoteEvent;

        public SoundManager(Analysis page)
        {
            this._page = page;
            _microphone.BufferReady -= microphone_BufferReady;
            _microphone.BufferReady += microphone_BufferReady;
            if(_microphone.BufferDuration==TimeSpan.Zero)
            _microphone.BufferDuration = TimeSpan.FromMilliseconds(100);
            _buffer = new byte[_microphone.GetSampleSizeInBytes(_microphone.BufferDuration)];
            _notesList = new List<SoundNote>();
            AchievedNotes= new List<SoundNote>();
            _microphone.Start();
        }

        private void microphone_BufferReady(object sender, EventArgs e)
        {
            _page.MessageTextBlock.Text = "";
            _microphone.GetData(_buffer);
            double[] x = new double[2048];
            int index = 0;
            for (int i = 0; i < 2048; i += 2)
            {
                x[index] = Convert.ToDouble(BitConverter.ToInt16(_buffer, i));
                index++;
            }

            double frequency = FrequencyUtils.FindFundamentalFrequency(x, _microphone.SampleRate, 60, 2000);

            SoundNote soundNote = new SoundNote();
            soundNote.FindNoteByFrequency(frequency);
            _notesList.Add(soundNote);
            _page.ShowNote(soundNote);
            if (!soundNote.IsOutOfTune)
            {
                if (IsCorrectListOfNotes(_notesList))
                {
                    a = x;
                    double[] b = FrequencyUtils.Spectr;
                    SoundNote lastNote = _notesList.Last();
                    AchievedNotes.Add(lastNote);
                    _notesList.Clear();
                    CorrectNoteEvent(_microphone);
                }
            }

        }

        private bool IsCorrectListOfNotes(List<SoundNote> listOfNotes)
        {
            if (_notesList.Count > 5)
            {
                SoundNote[] arrayOfNotes = listOfNotes.ToArray();
                for (int i = arrayOfNotes.Length - 1; i > arrayOfNotes.Length -6; i--)
                {
                    if (i == arrayOfNotes.Length - 1) continue;
                    if (i == arrayOfNotes.Length - 5)  return true;
                    if (!arrayOfNotes[i].Equals(arrayOfNotes[i + 1])) return false;
                }
            }
            return false;
        }

        public void FindHighestAndLowestNote()
        {
            HighestNote = AchievedNotes.Max();
            LowestNote = AchievedNotes.Min();
        }

    }
}