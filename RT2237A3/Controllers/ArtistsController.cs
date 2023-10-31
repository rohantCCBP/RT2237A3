using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Controllers
{
    public class ArtistsController : Controller
    {
        Manager m = new Manager();
        // GET: Artists
        public ActionResult Index()
        {
            var artists = m.ArtistGetAll();
            return View(artists);
        }
    }
}
