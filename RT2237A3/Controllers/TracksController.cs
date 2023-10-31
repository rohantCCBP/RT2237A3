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
        [ActionName("CreateForm")]
        public ActionResult Create(TrackAddFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Convert form model to the ViewModel
                var convertedModel = ConvertToTrackAddViewModel(model);  // Using 'model' and renaming the result

                // Validate the MediaTypeId
                if (!m.MediaTypeExists(convertedModel.MediaTypeId))
                {
                    ModelState.AddModelError("MediaTypeId", "Selected Media Type is invalid.");
                }
                else
                {
                    var newTrack = m.TrackAdd(convertedModel);  // Use 'convertedModel'

                    if (newTrack != null)
                        return RedirectToAction("Index");
                }
            }

            // If we reach here, something failed, redisplay form
            model.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
            model.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");

            return View(model);
        }


        private TrackAddViewModel ConvertToTrackAddViewModel(TrackAddFormViewModel formModel)
        {
            return new TrackAddViewModel
            {
                Name = formModel.Name,
                Composer = formModel.Composer,
                Milliseconds = formModel.Milliseconds,
                UnitPrice = formModel.UnitPrice,
                MediaTypeId = int.Parse(formModel.MediaTypeList.SelectedValue.ToString()), // Assuming you're storing the MediaTypeId as the selected value
                                                                                           // ... any other properties
            };
        }


        [HttpPost]
        public ActionResult Create(TrackAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var addedTrack = m.TrackAdd(model);

                if (addedTrack != null)
                    return RedirectToAction("Details", new { id = addedTrack.TrackId });
            }

            // If we reach here, something failed, re-populate SelectList and redisplay form
            var form = new TrackAddFormViewModel
            {
                Name = model.Name,
                Composer = model.Composer,
                Milliseconds = model.Milliseconds,
                UnitPrice = model.UnitPrice,
                AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title"),
                MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name")
            };
            return View(form);
        }
    }
}