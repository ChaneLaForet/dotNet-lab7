using AutoMapper;
using Lab2.Data;
using Lab2.Models;
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
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlaylistsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlaylistsController(ApplicationDbContext context, ILogger<PlaylistsController> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddPlaylist(NewPlaylistRequest newPlaylistRequest)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            List<Movie> addedMovies = new List<Movie>();

            newPlaylistRequest.MovieIds.ForEach(mid =>
            {
                var movieWithId = _context.Movies.Find(mid);
                if (movieWithId != null)
                {
                    addedMovies.Add(movieWithId);
                }
            });

            if (addedMovies.Count == 0)
            {
                return BadRequest();
            }

            var playlist = new Playlist
            {
                ApplicationUser = user,
                PlaylistDateTime = newPlaylistRequest.PlaylistDateTime.GetValueOrDefault(),
                PlaylistName = newPlaylistRequest.PlaylistName,
                Movies = addedMovies
            };

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return Ok();
        }

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

    }
}
