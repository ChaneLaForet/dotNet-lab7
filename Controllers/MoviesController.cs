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

        /// <summary>
        /// Returns a list of movies.
        /// </summary>
        /// <returns>All the movies.</returns>
        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieViewModel>>> GetMovies()
        {
            var movies = await _context.Movies.Select(m => _mapper.Map<MovieViewModel>(m)).ToListAsync();
            return movies;
        }

        /// <summary>
        /// Returns a movie with the given Id.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <returns>The movie or NotFound.</returns>
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

        /// <summary>
        /// Returns a list of movies filtered based on the date they were added and ordered descendingly by their release year.
        /// </summary>
        /// <param name="fromDate">The lowest (earliest) valid value a movie's DateAdded may have.</param>
        /// <param name="toDate">The highest (latest) valid value a movie's DateAdded may have.</param>
        /// <returns>The list of filtered and ordered movies.</returns>
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

        /// <summary>
        /// Returns a movie and its comments, based on the given movie's Id.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <returns>A movie and its comments</returns>
        //https://localhost:5001/api/movies/1/comments
        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<MovieWithCommentsViewModel>> GetCommentsForMovie(int id)
        {
            var query = _context.Movies.Where(m => m.Id == id).Include(m => m.Comments).Select(m => _mapper.Map<MovieWithCommentsViewModel>(m));

            return query.ToList();
        }

        /// <summary>
        /// Adds a comment to a movie.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Ok if comment was added, or NotFound if the movie was not found (based on Id.)</returns>
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

        /// <summary>
        /// Updates a movie.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <param name="movie">The movie.</param>
        /// <returns>NoContent if movie was added, BadRequest if the Id is not valid, or NotFound if movie was not found (based on Id).</returns>
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

        /// <summary>
        /// Updates a comment.
        /// </summary>
        /// <param name="commentId">The comment Id.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>NoContent if comment was added, BadRequest if the Id is not valid, or NotFound if comment was not found (based on Id).</returns>
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

        /// <summary>
        /// Adds a movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>The movie, if it was successfully added, BadRequest otherwise.</returns>
        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieViewModel movie)
        {
            _context.Movies.Add(_mapper.Map<Movie>(movie));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        /// <summary>
        /// Deletes a movie.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <returns>NoContent if the movie was deleted successfully, or NotFound otherwise.</returns>
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

        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="commentId">The comment Id.</param>
        /// <returns>NoContent if the comment was deleted successfully, or NotFound otherwise.</returns>
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

        /// <summary>
        /// Checks if a movie exists.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <returns>true if movie exists, false otherwise</returns>
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        /// <summary>
        /// Checks if a comment exists.
        /// </summary>
        /// <param name="id">The comment Id.</param>
        /// <returns>true if comment exists, false otherwise</returns>
        private bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
    }
}
