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
        public async Task<ActionResult<PaginatedResultSet<Playlist>>> GetAll(int? page = 1, int? perPage = 20)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            var serviceResponse = await _playlistService.GetAll(user.Id, page, perPage);

            return serviceResponse.ResponseOk;
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

        // PUT: https://localhost:5001/api/playlists/9
        [HttpPut("{playlistId}")]
        public async Task<ActionResult> UpdatePlaylist(int playlistId, UpdatedPlaylistViewModel updatedPlaylistViewModel)
        {
            if (playlistId != updatedPlaylistViewModel.Id)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            if (!_playlistService.PlaylistExists(playlistId))
            {
                return NotFound();
            }

            var playlistResponse = await _playlistService.GetPlaylistById(user.Id, updatedPlaylistViewModel.Id);

            Playlist playlist = playlistResponse.ResponseOk;


            var serviceResponse = await _playlistService.UpdatePlaylist(playlist, updatedPlaylistViewModel);

            if (serviceResponse.ResponseError != null)
            {
                return BadRequest(serviceResponse.ResponseError);
            }

            return Ok();
        }
    }
}
