using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Models
{
      public class PlaylistWithTracksViewModel 
    {
       public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
      }
}