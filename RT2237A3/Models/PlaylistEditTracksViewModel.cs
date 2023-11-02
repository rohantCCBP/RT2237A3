using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Models
{
    public class PlaylistEditTracksViewModel
    {
        public int Id { get; set; }
        public IEnumerable<int> SelectedTrackIds { get; set; }
        public MultiSelectList TrackSelectionsList { get; set; }

        public PlaylistEditTracksViewModel()
        {
            SelectedTrackIds = new List<int>();
        }
    }



}