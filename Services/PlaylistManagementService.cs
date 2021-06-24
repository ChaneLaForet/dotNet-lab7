using Lab2.Data;
using Lab2.Errors;
using Lab2.Models;
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

        public async Task<ServiceResponse<List<Playlist>, IEnumerable<PlaylistError>>> GetAll(string userId)
        {
            var result = await _context.Playlists.Where(p => p.ApplicationUser.Id == userId).Include(p => p.Movies).OrderBy(p => p.Id).ToListAsync();
            var serviceResponse = new ServiceResponse<List<Playlist>, IEnumerable<PlaylistError>>();
            serviceResponse.ResponseOk = result;
            return serviceResponse;
        }

        /*
        // GET: https://localhost:5001/api/playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistsForUserResponse>>> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            var result = _context.Playlists.Where(p => p.ApplicationUser.Id == user.Id).Include(p => p.Movies)
                .OrderBy(p => p.Id)
                .ToList();

            return _mapper.Map<List<Playlist>, List<PlaylistsForUserResponse>>(result);
        }
         */

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }

        public bool PlaylistExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
    }
}
