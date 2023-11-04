using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Models
{
    public class TrackAddFormViewModel
    {
        [Display(Name = "Track name")]
        public string Name { get; set; }

            public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        // DROPDOWN LISTS
        public SelectList AlbumList { get; set; }
        public SelectList MediaTypeList { get; set; }

        public int MediaTypeId { get; set; }
    }
}