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
            var playlists = m.PlaylistGetAll(); // Fetch all playlists sorted by name
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
            //if (playlist == null)
            //{
            //    return NotFound();
            //}

            var allTracks = m.PlaylistGetAll();
            var selectedTrackIds = playlist.Tracks.Select(t => t.TrackId);



            var formModel = new PlaylistEditTracksFormViewModel
            {
                Id = playlist.PlaylistId,
                Name = playlist.Name,
                 Tracks = playlist.Tracks,

//TracksList = new MultiSelectList(allTracks, "Id", "NameFull", selectedTrackIds),
                //formModel.CurrentTracks = new MultiSelectList(allTracks, "Id", "NameFull", selectedTrackIds),


                CurrentTracks = playlist.Tracks.Select(t => new TrackBaseViewModel
                {
                    TrackId = t.TrackId,
                    Name = t.Name,
                    Composer = t.Composer,
                    Milliseconds = t.Milliseconds,
                    UnitPrice = t.UnitPrice
                })
            };

            return View(formModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, PlaylistEditTracksViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Your logic to update the playlist with the selected tracks
                m.UpdatePlaylistTracks(id, model.SelectedTracks);
                return RedirectToAction("Details", new { id = id });
            }
            return View(model);
        }


    }
}