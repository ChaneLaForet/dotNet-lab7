using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Data;
using Lab2.Models;
using Lab2.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MoviesController> _logger;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context, ILogger<MoviesController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieViewModel>>> GetMovies()
        {
            var movies = await _context.Movies.Select(m => _mapper.Map<MovieViewModel>(m)).ToListAsync();
            return movies;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieViewModel>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return _mapper.Map<MovieViewModel>(movie);
        }

        //https://localhost:5001/api/movies/sortByDateAdded/2000-01-20&2019-01-30
        //GET: api/Movies/SortByDateAdded
        [HttpGet]
        [Route("sortByDateAdded/{fromDate}&{toDate}")]
        public async Task<ActionResult<IEnumerable<MovieViewModel>>> SortByDateAdded(DateTime fromDate, DateTime toDate)
        {
            var query = await _context.Movies.Where(m => m.DateAdded.CompareTo(fromDate) >= 0 && m.DateAdded.CompareTo(toDate) <= 0)
                                             .OrderByDescending(m => m.YearOfRelease)
                                             .Select(m => _mapper.Map<MovieViewModel>(m)).ToListAsync();
            return query;
        }

        //https://localhost:5001/api/movies/1/comments
        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<MovieWithCommentsViewModel>> GetCommentsForMovie(int id)
        {
            var query = _context.Movies.Where(m => m.Id == id).Include(m => m.Comments).Select(m => _mapper.Map<MovieWithCommentsViewModel>(m));

            return query.ToList();
        }

        //https://localhost:5001/api/movies/1/comments
        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForMovie(int id, CommentViewModel comment)
        {
            var movie = _context.Movies.Where(m => m.Id == id).Include(m => m.Comments).FirstOrDefault();
            if (movie == null)
            {
                return NotFound();
            }

            movie.Comments.Add(_mapper.Map<Comment>(comment));
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieViewModel movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Movie>(movie)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //https://localhost:5001/api/movies/1/comments/1
        //Don't forget to write comment Id in the request body
        [HttpPut("{id}/Comments/{commentId}")]
        public async Task<IActionResult> PutComment(int commentId, CommentViewModel comment)
        {
            if (commentId != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Comment>(comment)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(commentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieViewModel movie)
        {
            _context.Movies.Add(_mapper.Map<Movie>(movie));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //https://localhost:5001/api/movies/3/comments/2
        [HttpDelete("{id}/Comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
    }
}
