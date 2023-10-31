using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT2237A3.Models
{
    public class TrackBaseViewModel
    {
        [Key] 
        public int TrackId { get; set; }

        [Display(Name = "Track name")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        //public int MediaTypeId { get; set; }

        //public int? GenreId { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        //public int? Bytes { get; set; }

        [Display(Name = "Unit price")]
        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }



        public string NameFull { get; set; }
        public string NameShort { get; set; }
    }





}