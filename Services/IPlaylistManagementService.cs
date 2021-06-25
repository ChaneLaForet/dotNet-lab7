using Lab2.Errors;
using Lab2.Models;
using Lab2.ViewModels;
using Lab2.ViewModels.Playlists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface IPlaylistManagementService
    {
        public bool PlaylistExists(int id);
        public bool MovieExists(int id);
        public Task<ServiceResponse<PaginatedResultSet<Playlist>, IEnumerable<PlaylistError>>> GetAll(string userId, int? page = 1, int? perPage = 10);
        public Task<ServiceResponse<Playlist, IEnumerable<PlaylistError>>> GetPlaylistById(string userId, int id);
        public Task<ServiceResponse<bool, IEnumerable<PlaylistError>>> DeletePlaylist(int id);
        public Task<ServiceResponse<Playlist, IEnumerable<PlaylistError>>> AddPlaylist(string userId, NewPlaylistRequest newPlaylistRequest);
        public Task<ServiceResponse<Playlist, IEnumerable<PlaylistError>>> UpdatePlaylist(Playlist playlist, UpdatedPlaylistViewModel updatedPlaylistViewModel);
    }
}
