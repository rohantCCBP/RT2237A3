using AutoMapper;
using RT2237A3.Data;
using RT2237A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-e943f9f1-bf57-40da-b2ba-8ebfe46db6bf
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace RT2237A3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>()
     .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MediaType))
     .ForMember(dest => dest.AlbumArtistName, opt => opt.MapFrom(src => src.Album.Artist.Name))
     .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album.Title));
                cfg.CreateMap<TrackAddFormViewModel, Track>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                //cfg.CreateMap<Playlist, PlaylistBaseViewModel>().ForMember(dest => dest.TracksCount, opt => opt.MapFrom(src => src.Tracks.Count));
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>()
   .ForMember(dest => dest.TrackNames, opt => opt.MapFrom(src => src.Tracks.Select(t => t.Name)));



            })
            {
            };
            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = ds.Albums.OrderBy(a => a.Title).ToList();
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albums);
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artists = ds.Artists.OrderBy(a=> a.Name).ToList();
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artists);
        }

        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            var mediaTypes = ds.MediaTypes.OrderBy(m=>m.Name).ToList();
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(mediaTypes);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetAll()
        {
            var tracks = ds.Tracks.Include("Album.Artist").Include("MediaType").OrderBy(t=> t.Name).ToList();
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(tracks);
        }

        public TrackWithDetailViewModel TrackGetOne(int id)
        {
            //var track = ds.Tracks.Include("MediaType").SingleOrDefault(t => t.TrackId == id);
            var track = ds.Tracks.Include("MediaType").Include("Album.Artist").SingleOrDefault(t => t.TrackId == id);

            if (track == null) return null;

            var result = mapper.Map<TrackWithDetailViewModel>(track);

            return result;
        }

        public TrackBaseViewModel TrackAdd(TrackAddViewModel model)
        {
            // Validation
            if (model.MediaTypeId <= 0 || !ds.MediaTypes.Any(m => m.MediaTypeId == model.MediaTypeId))
            {
                // Handle error - MediaTypeId is not set or is invalid
                return null;
            }
            // Additional validations for other properties like AlbumId can be added here

            Track newTrack = mapper.Map<Track>(model); // Assuming you have mapping set up
            ds.Tracks.Add(newTrack);

            ds.SaveChanges();

            return mapper.Map<Track, TrackBaseViewModel>(newTrack);
        }

        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            var playlists = ds.Playlists.Include("Tracks").OrderBy(p => p.Name).ToList();

            return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(playlists);
        }

        //ORIGINAL
        //public PlaylistBaseViewModel GetPlaylistWithTracks(int id)
        //{
        //    var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == id);
        //    if (playlist == null) return null;

        //    var result = new PlaylistBaseViewModel
        //    {
        //        PlaylistId = playlist.PlaylistId,
        //        Name = playlist.Name,
        //        TrackNames = playlist.Tracks.Select(t => t.Name).ToList()
        //    };

        //    return result;
        //}

        public PlaylistEditTracksViewModel GetPlaylistWithTracks(int id)
        {
            var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == id);
            if (playlist == null) return null;

            var result = new PlaylistEditTracksViewModel
            {
                Id = playlist.PlaylistId,
                Name = playlist.Name,
                TracksOnPlaylist = playlist.Tracks.Select(t => new TrackBaseViewModel
                {
                    // Map properties of t (which is a Track) to this ViewModel
                    TrackId = t.TrackId,
                    // ... (any other properties your TrackBaseViewModel might have, map them here)
                }).ToList(),// Directly assign the tracks from the fetched playlist
                //SelectedTracks = playlist.Tracks.Select(t => t.PlaylistId).ToList() // IDs of tracks
                SelectedTracks = playlist.Tracks.Select(t => t.TrackId).ToList(),

            };

            return result;
        }
        public List<TrackBaseViewModel> GetAllTracks()
        {
            return ds.Tracks.OrderBy(t => t.Name).Select(t => new TrackBaseViewModel
            {
                TrackId = t.TrackId,
                // ... (map all other properties of TrackBaseViewModel from t)
            }).ToList();
        }















        //public PlaylistBaseViewModel PlaylistGetById(int id)
        //{
        //    var playlist = ds.Playlists.SingleOrDefault(p => p.PlaylistId == id);
        //    if (playlist == null) return null;

        //    return mapper.Map<Playlist, PlaylistBaseViewModel>(playlist);
        //}

        //// This hypothetical method fetches tracks associated with a given playlist
        //public IEnumerable<TrackCheckBoxListViewModel> GetTracksForPlaylist(int id)
        //{
        //    var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == id);
        //    if (playlist == null) return null;

        //    return mapper.Map<IEnumerable<Track>, IEnumerable<TrackCheckBoxListViewModel>>(playlist.Tracks);
        //}

        //// Update the tracks of a specific playlist
        //public bool PlaylistEditTracks(int playlistId, List<int> selectedTrackIds)
        //{
        //    var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == playlistId);
        //    if (playlist == null) return false;

        //    // Clearing the current tracks from the playlist
        //    playlist.Tracks.Clear();

        //    // Adding the selected tracks to the playlist
        //    foreach (var trackId in selectedTrackIds)
        //    {
        //        var track = ds.Tracks.SingleOrDefault(t => t.TrackId == trackId);
        //        if (track != null) playlist.Tracks.Add(track);
        //    }

        //    // Saving the changes to the database
        //    ds.SaveChanges();

        //    return true;
        //}

        internal bool MediaTypeExists(int mediaTypeId)
        {
            return ds.MediaTypes.Any(mt => mt.MediaTypeId == mediaTypeId);
        }

    }
}