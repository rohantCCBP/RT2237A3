using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT2237A3.Models
{
    public class TrackAddViewModel
    {
        [Display(Name = "Track name")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        [Required]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit price")]
        [Required]
        public decimal UnitPrice { get; set; }

        // Replace SelectList with int properties
        [Range(1, int.MaxValue, ErrorMessage = "Please select an Album.")]
        public int AlbumId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a Media Type.")]
        public int MediaTypeId { get; set; }
    }

}