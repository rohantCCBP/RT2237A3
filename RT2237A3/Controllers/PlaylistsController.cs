using RT2237A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace RT2237A3.Controllers
{
    public class PlaylistsController : Controller
    {
        Manager m = new Manager();
        // GET: Playlists
        public ActionResult Index()
        {
            var playlists = m.PlaylistGetAll();
            return View(playlists);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var playlist = m.PlaylistGetById(id.Value);

            if (playlist == null)
            {
                return HttpNotFound();
            }

            return View(playlist);
        }

        public ActionResult Edit(int id)
        {
            var playlist = m.PlaylistGetById(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }

            var selectedTrackIds = playlist.Tracks.Select(t => t.TrackId).ToList();

            var allTracks = m.TrackGetAll();
            var trackSelectionsList = new MultiSelectList(allTracks, "TrackId", "NameFull", selectedTrackIds);

            var formModel = new PlaylistEditTracksFormViewModel
            {
                Id = playlist.PlaylistId,
                Name = playlist.Name,
                Tracks = playlist.Tracks,
                CurrentTracks = playlist.Tracks.Select(t => new TrackBaseViewModel
                {
                    TrackId = t.TrackId,
                    Name = t.NameShort,
                    Composer = t.Composer,
                    Milliseconds = t.Milliseconds,
                    UnitPrice = t.UnitPrice
                }),
                TrackSelectionsList = trackSelectionsList
            };

            return View(formModel);
        }

        [HttpPost]
        public ActionResult Edit(PlaylistEditTracksViewModel model)
        {
            if (model.SelectedTrackIds == null)
            {
                model.SelectedTrackIds = new int[0];
            }

            if (ModelState.IsValid)
            {
                m.UpdatePlaylistTracks(model.Id, model.SelectedTrackIds);
                return RedirectToAction("Details", new { id = model.Id });
            }

            return View(model);
        }



    }
}