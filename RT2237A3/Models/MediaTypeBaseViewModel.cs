using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT2237A3.Models
{
    public class MediaTypeBaseViewModel
    {
        [Key]
        public int MediaTypeId { get; set; }

        [StringLength(120)]
        [Display(Name = "Media type")]
        public string Name { get; set; }

    }
}