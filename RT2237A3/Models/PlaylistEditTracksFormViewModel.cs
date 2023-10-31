using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Models
{
    //public class PlaylistEditTracksFormViewModel
    //{
    //    public int PlaylistId { get; set; }
    //    public string PlaylistName { get; set; }
    //    public MultiSelectList TracksSelectList { get; set; }
    //    public List<TrackCheckBoxListViewModel> AllTracks { get; set; }
    //    public IEnumerable<TrackCheckBoxListViewModel> CurrentPlaylistTracks { get; set; }
    //}
    public class PlaylistEditTracksFormViewModel 
    {
        public int Id { get; set; } // Playlist Identifier
        public string Name { get; set; } // Playlist Name for display purposes

        // All tracks for selection
        public MultiSelectList TracksList { get; set; }

        // Tracks currently associated with the playlist
        public IEnumerable<TrackBaseViewModel> CurrentTracks { get; set; }
    }
}