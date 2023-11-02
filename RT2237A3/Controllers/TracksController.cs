using RT2237A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RT2237A3.Controllers
{
    public class TracksController : Controller
    {
        Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = m.TrackGetAll();
            return View(tracks);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrackWithDetailViewModel track = m.TrackGetOne(id.Value);

            if (track == null)
            {
                return HttpNotFound();
            }

            return View(track);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var form = new TrackAddFormViewModel
            {
                AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title"),
                MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name")
            };

            return View(form);
        }


        [HttpPost]
        public ActionResult Create(TrackAddViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (!m.MediaTypeExists(model.MediaTypeId))
                {
                    ModelState.AddModelError("MediaTypeId", "Selected Media Type is invalid.");
                }
                else
                {
                    var newTrack = m.TrackAdd(model);
                    if (newTrack != null)
                        return RedirectToAction("Details", new { id = newTrack.TrackId });
                }
            }

            return View(model);
        }
    }
}