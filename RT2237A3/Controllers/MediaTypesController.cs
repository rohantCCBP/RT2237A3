using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Controllers
{
    public class MediaTypesController : Controller
    {
        Manager m = new Manager();
        // GET: MediaTypes
        public ActionResult Index()
        {
            var mediaTypes = m.MediaTypeGetAll();
            return View(mediaTypes);
        }
    }
}
