using System;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using Windows.Foundation.Metadata;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MainApp
{
    public partial class Analysis
    {
        private readonly SoundManager _soundManager;
        private int iter = 0;
        public Analysis()
        {
            InitializeComponent();

            var dispTimer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(50)};
            dispTimer.Tick += delegate
            {
                try
                {
                    FrameworkDispatcher.Update();
                }
                catch
                {
                }
            };
            dispTimer.Start();
            MessageTextBlock.Text = "";
            NoteTextBlock.Text = "";
            CommandTextBlock.Text = "Sing musical notes in the order - as high as you can and as low as you can. If you can't - just click \"STOP\"";
            _soundManager = new SoundManager(this);
            _soundManager.CorrectNoteEvent += OnCorrectNote;
        }

        public void ShowNote(SoundNote soundNote)
        {
            iter++;
            NoteTextBlock.Foreground = soundNote.IsOutOfTune
                ? new SolidColorBrush(Colors.Red)
                : new SolidColorBrush(Colors.Green);
            NoteTextBlock.Text = soundNote.Note + soundNote.Octave;
        }

        public void OnCorrectNote(Microphone mic)
        {
            MessageTextBlock.Text = "Ok, sing next note";
            mic.Stop();
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            mic.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
                _soundManager.FindHighestAndLowestNote();
            string highestNote;
            string lowestNote;

            highestNote = _soundManager.HighestNote.ToString();
            lowestNote = _soundManager.LowestNote.ToString();
            if (lowestNote[1] == '#')
            {
                    string tempNote = lowestNote[0] + "is" + lowestNote[2];
                    lowestNote = tempNote;
            }
            if (highestNote[1] == '#')
            {
                string tempNote = highestNote[0] + "is" + highestNote[2];
                highestNote = tempNote;
            }
                NavigationService.Navigate(
                    new Uri(
                        "/Result.xaml?lowestnote=" + lowestNote + "&highestnote=" +
                        highestNote, UriKind.Relative));
            
        }

    }
}