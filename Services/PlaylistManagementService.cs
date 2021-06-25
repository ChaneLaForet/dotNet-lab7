using Lab2.Data;
using Lab2.Errors;
using Lab2.Models;
using Lab2.ViewModels;
using Lab2.ViewModels.Playlists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class PlaylistManagementService : IPlaylistManagementService
    {
        public ApplicationDbContext _context;

        public PlaylistManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<PaginatedResultSet<Playlist>, IEnumerable<PlaylistError>>> GetAll(string userId, int? page = 1, int? perPage = 10)
        {
            var playlists = await _context.Playlists.AsNoTracking().Where(p => p.ApplicationUser.Id == userId).Include(p => p.Movies).OrderBy(p => p.Id).ToListAsync();

            var count = await _context.Playlists.Where(p => p.ApplicationUser.Id == userId).CountAsync();
            var resultSet = new PaginatedResultSet<Playlist>(playlists, page.Value, count, perPage.Value);

            var serviceResponse = new ServiceResponse<PaginatedResultSet<Playlist>, IEnumerable<PlaylistError>>();
            serviceResponse.ResponseOk = resultSet;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Playlist, IEnumerable<PlaylistError>>> GetPlaylistById(string userId, int playlistId)
        {
            var playlist = await _context.Playlists.Where(p => p.ApplicationUser.Id == userId && p.Id == playlistId)
                .Include(p => p.Movies).FirstOrDefaultAsync();
            var serviceResponse = new ServiceResponse<Playlist, IEnumerable<PlaylistError>>();
            serviceResponse.ResponseOk = playlist;

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool, IEnumerable<PlaylistError>>> DeletePlaylist(int id)
        {
            var serviceResponse = new ServiceResponse<bool, IEnumerable<PlaylistError>>();

            try
            {
                var playlist = await _context.Playlists.FindAsync(id);
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = true;
            }
            catch (Exception e)
            {
                var errors = new List<PlaylistError>();
                errors.Add(new PlaylistError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Playlist, IEnumerable<PlaylistError>>> AddPlaylist(string userId, NewPlaylistRequest newPlaylistRequest)
        {
            var serviceResponse = new ServiceResponse<Playlist, IEnumerable<PlaylistError>>();

            var addedMovies = new List<Movie>();
            newPlaylistRequest.MovieIds.ForEach(mid =>
            {
                var movieWithId = _context.Movies.Find(mid);
                if (movieWithId != null)
                {
                    addedMovies.Add(movieWithId);
                }
            });

            var playlist = new Playlist
            {
                ApplicationUserId = userId,
                Movies = addedMovies,
                PlaylistName = newPlaylistRequest.PlaylistName,
                PlaylistDateTime = newPlaylistRequest.PlaylistDateTime.GetValueOrDefault(),
            };

            _context.Playlists.Add(playlist);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = playlist;
            }
            catch (Exception e)
            {
                var errors = new List<PlaylistError>();
                errors.Add(new PlaylistError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Playlist, IEnumerable<PlaylistError>>> UpdatePlaylist(Playlist playlist, UpdatedPlaylistViewModel updatedPlaylistViewModel)
        {
            playlist.PlaylistName = updatedPlaylistViewModel.PlaylistName;
            playlist.Movies = await _context.Movies.Where(m => updatedPlaylistViewModel.MovieIds.Contains(m.Id)).ToListAsync();

            _context.Entry(playlist).State = EntityState.Modified;

            var serviceResponse = new ServiceResponse<Playlist, IEnumerable<PlaylistError>>();

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = playlist;
            }
            catch (Exception e)
            {
                var errors = new List<PlaylistError>();
                errors.Add(new PlaylistError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }

        public bool PlaylistExists(int id)
        {
            return _context.Playlists.Any(p => p.Id == id);
        }
    }
}
