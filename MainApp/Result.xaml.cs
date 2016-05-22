using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MainApp
{
    public partial class Result
    {
        private string _lowestNote;
        private string _highestNote;

        public Result()
        {
            InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("lowestnote", out _lowestNote))
            {
                if (_lowestNote.Length>2)
                {
                    string tempNote = _lowestNote[0] + "#" + _lowestNote[3];
                    _lowestNote = tempNote;
                }
                LowestNoteTextBlock.Text += _lowestNote;
            }
            if (NavigationContext.QueryString.TryGetValue("highestnote", out _highestNote)) 
            {
                if (_highestNote.Length>2)
                {
                    string tempNote = _highestNote[0] + "#" + _highestNote[3];
                    _highestNote = tempNote;
                }
                HighestNoteTextBlock.Text +=_highestNote;
            }
            VocalManager vocalManager =new VocalManager("Resources/VocalRanges.xml");
            List<VocalRange> listOfRanges = vocalManager.GetRangeOrRanges(_lowestNote, _highestNote);
            VocalRangeTextBlock.Text = GenerateTextOfVocalRange(listOfRanges);
        }

        private string GenerateTextOfVocalRange(List<VocalRange> listOfVocalRanges )
        {
            if (listOfVocalRanges.Count == 1)
            {
                return "Your vocal range is\n" + listOfVocalRanges[0].Name;
            }
            if (listOfVocalRanges.Count == 2)
            {
                return "Your lowest note is from \n" + listOfVocalRanges[0].Name +
                                           "\nYour highest note is from \n" + listOfVocalRanges[1].Name;
            }

            if (listOfVocalRanges.Count == 4 && listOfVocalRanges[1].Name.Equals(""))
            {
                return "Your lowest note is from \n" + listOfVocalRanges[0].Name +
                                           "\n Your highest note is between\n" + listOfVocalRanges[2].Name + "\n and\n" +
                                           listOfVocalRanges[3].Name;
            }
            if (listOfVocalRanges.Count == 4 && listOfVocalRanges[2].Name.Equals(""))
            {
                return "Your lowest note is between\n" + listOfVocalRanges[0].Name + "\n and\n" +
                                           listOfVocalRanges[1].Name + "\n Your highest note is from\n" +
                                           listOfVocalRanges[3].Name;
            }
            if (listOfVocalRanges.Count == 4)
            {
                return "Your lowest note is between\n" + listOfVocalRanges[0].Name + "\n and\n" +
                                           listOfVocalRanges[1].Name + "\nYour highest note is between\n" + listOfVocalRanges[2].Name + "\n and\n" +
                                           listOfVocalRanges[3].Name;
            }
            else return "Unexpected error";
        }
    }
}