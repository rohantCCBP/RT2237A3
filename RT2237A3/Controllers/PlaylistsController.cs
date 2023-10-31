using RT2237A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue) return HttpNotFound();

            var playlist = m.GetPlaylistWithTracks(id.Value);
            if (playlist == null) return HttpNotFound();

            return View(playlist);
        }



        // GET: Playlists/EditTracks/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) return HttpNotFound();

        
            var viewModel = m.GetPlaylistWithTracks(id.Value);

            if (viewModel == null) return HttpNotFound();

            viewModel.AllTracks = m.GetAllTracks();

            return View(viewModel);
        }



        // POST: Playlists/EditTracks/5
        [HttpPost]
        public ActionResult Edit(PlaylistEditTracksViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Save changes, including updating tracks associated with the playlist

                return RedirectToAction("Details", new { id = viewModel.Id });
            }
            return View(viewModel);
        }
    }
}