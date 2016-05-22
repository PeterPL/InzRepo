using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation.Metadata;

namespace MainApp
{
    public class VocalManager
    {
        private List<VocalRange> _listOfAllRanges;

        public VocalManager(string vocalRangesResource)
        {
            _listOfAllRanges=new List<VocalRange>();
            XDocument rangesFile = XDocument.Load(vocalRangesResource);

            List<VocalRange> ranges = (from _range in rangesFile.Element("vocalranges").Elements("range")
                select new VocalRange()
                {
                    Name = _range.Element("name").Value,
                    LowestNote = _range.Element("lowestnote").Value,
                    HighestNote = _range.Element("highestnote").Value,
                    MaleOrFemale = _range.Element("maleorfemale").Value

                }).ToList();
            _listOfAllRanges = ranges;

        }

        public List<VocalRange> GetRangeOrRanges(string lowestNote, string highestNote)
        {
            var rangeWithTheSameLowestNote = new VocalRange();
            var rangeWithTheSameHighestNote = new VocalRange();

           List<VocalRange> listOfRanges = new List<VocalRange>();
            

            foreach (VocalRange range in _listOfAllRanges)
            {
                if (range.LowestNote == lowestNote) rangeWithTheSameLowestNote = range;
                if (range.HighestNote == highestNote) rangeWithTheSameHighestNote = range;
            }

            if (rangeWithTheSameLowestNote.Name == null && rangeWithTheSameHighestNote.Name == null)
            {
                
                VocalRange[] lowestNoteNeighbours = GetNeighboursOfNote(new SoundNote(lowestNote), true);
                VocalRange[] highestNoteNeighbours = GetNeighboursOfNote(new SoundNote(highestNote), false);

               listOfRanges.Add(lowestNoteNeighbours[0]);
               listOfRanges.Add(lowestNoteNeighbours[1]);
               listOfRanges.Add(highestNoteNeighbours[0]);
               listOfRanges.Add(highestNoteNeighbours[1]);
               return listOfRanges;

            }
            if (rangeWithTheSameLowestNote.Name == null)
            {
                VocalRange[] tempArray = GetNeighboursOfNote(new SoundNote(lowestNote), true);
                listOfRanges.Add(tempArray[0]);
                listOfRanges.Add(tempArray[1]);
                listOfRanges.Add(new VocalRange(){HighestNote = "", LowestNote = "", MaleOrFemale = "", Name=""});
                listOfRanges.Add(rangeWithTheSameHighestNote);
                return listOfRanges;
            }
            if (rangeWithTheSameHighestNote.Name == null)
            {
                VocalRange[] tempArray = GetNeighboursOfNote(new SoundNote(highestNote), false);
                listOfRanges.Add(rangeWithTheSameLowestNote);
                listOfRanges.Add(new VocalRange() { HighestNote = "", LowestNote = "", MaleOrFemale = "", Name = "" });
                listOfRanges.Add(tempArray[0]);
                listOfRanges.Add(tempArray[1]);
                
                return listOfRanges;
            }
            
            if (rangeWithTheSameLowestNote.Name.Equals(rangeWithTheSameHighestNote.Name))
            {
                listOfRanges.Add(rangeWithTheSameHighestNote);
                return listOfRanges;
            }
            if (!rangeWithTheSameLowestNote.Name.Equals(rangeWithTheSameHighestNote.Name))
            {
                listOfRanges.Add(rangeWithTheSameLowestNote);
                listOfRanges.Add(rangeWithTheSameHighestNote);
                return listOfRanges;
            }
            return null;
        }

        private VocalRange[] GetNeighboursOfNote(SoundNote soundNote, bool isFindingLowest)
        {
            VocalRange[] arrayOfRanges = new VocalRange[2];
            int distance;
            if (isFindingLowest)
            {
                distance = 100;
                for (int i = 0; i < _listOfAllRanges.Count; i++)
                {
                    SoundNote tempNote = new SoundNote(_listOfAllRanges[i].LowestNote);
                    int tempdistance = SoundNote.GetDistanceBetweenNotes(soundNote, tempNote);
                    if (tempdistance < distance && soundNote.CompareTo(tempNote) == 1)
                    {
                        distance = tempdistance;
                        arrayOfRanges[0] = _listOfAllRanges[i];
                    }
                }
                distance = 100;
                for (int i = 0; i < _listOfAllRanges.Count; i++)
                {
                    SoundNote tempNote = new SoundNote(_listOfAllRanges[i].LowestNote);
                    int tempdistance = SoundNote.GetDistanceBetweenNotes(soundNote, tempNote);
                    if (tempdistance < distance && soundNote.CompareTo(tempNote) == -1)
                    {
                        distance = tempdistance;
                        arrayOfRanges[1] = _listOfAllRanges[i];
                    }
                }
            }
            else
            {
                distance = 100;
                for (int i = 0; i < _listOfAllRanges.Count; i++)
                {
                    SoundNote tempNote = new SoundNote(_listOfAllRanges[i].HighestNote);
                    int tempdistance = SoundNote.GetDistanceBetweenNotes(soundNote, tempNote);
                    if (tempdistance < distance && soundNote.CompareTo(tempNote) == 1)
                    {
                        distance = tempdistance;
                        arrayOfRanges[0] = _listOfAllRanges[i];
                    }
                }
                distance = 100;
                for (int i = 0; i < _listOfAllRanges.Count; i++)
                {
                    SoundNote tempNote = new SoundNote(_listOfAllRanges[i].HighestNote);
                    int tempdistance = SoundNote.GetDistanceBetweenNotes(soundNote, tempNote);
                    if (tempdistance < distance && soundNote.CompareTo(tempNote) == -1)
                    {
                        distance = tempdistance;
                        arrayOfRanges[1] = _listOfAllRanges[i];
                    }
                }
            }

            return arrayOfRanges;
        }
    }

    public struct VocalRange 
    {
        public string Name;
        public string LowestNote;
        public string HighestNote;
        public string MaleOrFemale;
        
       
    }
}
