using Lab2.Errors;
using Lab2.Models;
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
        public Task<ServiceResponse<List<Playlist>, IEnumerable<PlaylistError>>> GetAll(string id);
        /*
         * public async Task<ActionResult> AddPlaylist(NewPlaylistRequest newPlaylistRequest)
         * public async Task<ActionResult<PlaylistsForUserResponse>> GetPlaylistById(int id)
         * public async Task<IActionResult> DeletePlaylist(int id)
         * public async Task<ActionResult> UpdatePlaylist(int id, UpdatedPlaylistViewModel updatedPlaylistViewModel)
         */
    }
}
