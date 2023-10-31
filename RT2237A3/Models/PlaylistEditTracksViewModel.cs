using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Models
{
    //public class PlaylistEditTracksViewModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //    // For tracks that are associated with the playlist
    //    public List<TrackBaseViewModel> TracksOnPlaylist { get; set; }

    //    // For all tracks
    //    public List<TrackBaseViewModel> AllTracks { get; set; }

    //    // For checkbox functionality - Ids of selected tracks
    //    public List<int> SelectedTracks { get; set; }
    //}
    public class PlaylistEditTracksViewModel
    {
        public int Id { get; set; }
        public IEnumerable<int> SelectedTracks { get; set; } // Ids of selected tracks
    }

}