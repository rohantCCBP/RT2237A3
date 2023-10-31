using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Models
{
      public class PlaylistEditTracksFormViewModel : PlaylistBaseViewModel 
    {
        public int Id { get; set; } // Playlist Identifier

        public string Name { get; set; }
        public IEnumerable<TrackBaseViewModel> CurrentTracks { get; set; }
        public IEnumerable<TrackCheckBoxListViewModel> TrackSelections { get; set; }
    
    }
}