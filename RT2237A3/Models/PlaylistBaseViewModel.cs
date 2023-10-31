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

        public IEnumerable<string> TrackNames { get; set; }

        [Display(Name = "Playlist Track Count")]
        public int TracksCount => TrackNames?.Count() ?? 0;

        public double Milliseconds { get; set; }
        public string Composer { get; set; }
        public decimal UnitPrice { get; set; }
        public ICollection<TrackBaseViewModel> Tracks { get; set; }

        // Composed read-only property to display full name.
        public string NameFull
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var composer = string.IsNullOrEmpty(Composer) ? "" : ", composer " + Composer;
                var trackLength = (ms > 0) ? ", " + ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? ", $ " + UnitPrice.ToString() : "";
                return string.Format("{0}{1}{2}{3}", Name, composer, trackLength, unitPrice);
            }
        }
        // Composed read-only property to display short name.
        public string NameShort
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var trackLength = (ms > 0) ? ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? " $ " + UnitPrice.ToString() : "";
                return string.Format("{0} - {1} - {2}", Name, trackLength, unitPrice);
            }
        }

    }

}