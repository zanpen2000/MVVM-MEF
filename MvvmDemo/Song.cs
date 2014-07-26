using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDemo
{
    public class Song
    {
        string songTitle;

        public string SongTitle
        {
            get { return songTitle; }
            set { songTitle = value; }
        }

        string artistName;

        public string ArtistName
        {
            get { return artistName; }
            set { artistName = value; }
        }
    }
}
