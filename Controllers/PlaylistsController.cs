using AutoMapper;
using Lab2.Data;
using Lab2.Models;
using Lab2.Services;
using Lab2.ViewModels;
using Lab2.ViewModels.Playlists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    public class PlaylistsController : ControllerBase
    {
        private readonly ILogger<PlaylistsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlaylistManagementService _playlistService;

        public PlaylistsController(ILogger<PlaylistsController> logger, IMapper mapper, UserManager<ApplicationUser> userManager, IPlaylistManagementService playlistService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _playlistService = playlistService;
        }

        // GET: https://localhost:5001/api/playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistsForUserResponse>>> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            var serviceResponse = await _playlistService.GetAll(user.Id);
            var playlists = serviceResponse.ResponseOk;

            return _mapper.Map<List<Playlist>, List<PlaylistsForUserResponse>>(playlists);
        }

        // GET: https://localhost:5001/api/playlists/1
        [HttpGet("{playlistId}")]
        public async Task<ActionResult<PlaylistsForUserResponse>> GetPlaylistById(int playlistId)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            if (!_playlistService.PlaylistExists(playlistId))
            {
                return NotFound();
            }

            var serviceResponse = await _playlistService.GetPlaylistById(user.Id, playlistId);
            var playlist = serviceResponse.ResponseOk;

            return _mapper.Map<PlaylistsForUserResponse>(playlist);
        }

        // DELETE: https://localhost:5001/api/playlists/9
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            var serviceResponse = await _playlistService.DeletePlaylist(id);

            if (serviceResponse.ResponseError != null)
            {
                return BadRequest(serviceResponse.ResponseError);
            }

            return NoContent();
        }

        // POST: https://localhost:5001/api/playlists
        [HttpPost]
        public async Task<ActionResult> AddPlaylist(NewPlaylistRequest newPlaylistRequest)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            var serviceResponse = await _playlistService.AddPlaylist(user.Id, newPlaylistRequest);

            if (serviceResponse.ResponseError != null)
            {
                return BadRequest(serviceResponse.ResponseError);
            }

            return Ok();
        }



        /*


        // PUT: https://localhost:5001/api/playlists/9
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlaylist(int id, UpdatedPlaylistViewModel updatedPlaylistViewModel)
        {
            if (id != updatedPlaylistViewModel.Id)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            List<Movie> addedMovies = new List<Movie>();

            updatedPlaylistViewModel.MovieIds.ForEach(mid =>
            {
                var movieWithId = _context.Movies.Find(mid);
                if (movieWithId != null)
                {
                    addedMovies.Add(movieWithId);
                }
            });

            Playlist playlist = _context.Playlists.Where(p => p.ApplicationUser.Id == user.Id && p.Id == id).Include(p => p.Movies).FirstOrDefault();

            if (playlist == null)
            {
                return BadRequest();
            }

            if (addedMovies != null)
            {
                playlist.Movies = addedMovies;
            }

            if (updatedPlaylistViewModel.PlaylistName != null)
            {
                playlist.PlaylistName = updatedPlaylistViewModel.PlaylistName;
            }

            playlist.ApplicationUserId = user.Id;

            _context.Entry(playlist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        */
    }
}
