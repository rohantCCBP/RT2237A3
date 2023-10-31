using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RT2237A3.Models
{
    public class TrackCheckBoxListViewModel
    {
        public int TrackId { get; set; }
        public string NameShort { get; set; }

        public string DisplayText { get; set; }
        public bool IsSelected { get; set; }
    }
}