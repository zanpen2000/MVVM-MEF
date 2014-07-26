using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDemo
{
    public class SongViewModel
    {
        private Song song;

        public Song Song
        {
            get { return song; }
            set { song = value; }
        }

        public string ArtistName { get { return Song.ArtistName; } set { Song.ArtistName = value; } }
    }
}
