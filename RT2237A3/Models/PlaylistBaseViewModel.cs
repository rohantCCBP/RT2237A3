using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT2237A3.Models
{
    public class PlaylistBaseViewModel
    {
        [Key]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        [Display(Name = "Playlist Name")]
        public string Name { get; set; }

        [Display(Name = "Playlist Track Count")]
        public int TracksCount => Tracks?.Count() ?? 0;

        public double Milliseconds { get; set; }
        public string Composer { get; set; }
        public decimal UnitPrice { get; set; }
        public ICollection<TrackBaseViewModel> Tracks { get; set; }

    }

}