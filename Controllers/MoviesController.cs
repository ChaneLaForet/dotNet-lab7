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
using Lab2.Services;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly ILogger<MoviesController> _logger;
        private readonly IMapper _mapper;
        private readonly IMovieManagementService _movieService;

        public MoviesController(ILogger<MoviesController> logger, IMapper mapper, IMovieManagementService movieService)
        {
            _logger = logger;
            _mapper = mapper;
            _movieService = movieService;
        }

        /// <summary>
        /// Returns a list of movies.
        /// </summary>
        /// <returns>All the movies.</returns>
        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieViewModel>>> GetMovies()
        {
            var serviceResult = await _movieService.GetMovies();
            var movies = serviceResult.ResponseOk;

            return _mapper.Map<IEnumerable<Movie>, List<MovieViewModel>>(movies);
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
            var serviceResult = await _movieService.GetMovie(id);
            var movie = serviceResult.ResponseOk;

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
            var serviceResult = await _movieService.SortByDateAdded(fromDate, toDate);
            var movies = serviceResult.ResponseOk;

            return _mapper.Map<IEnumerable<Movie>, List<MovieViewModel>>(movies);
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

            var serviceResult = await _movieService.PutMovie(id, _mapper.Map<Movie>(movie));

            if (!_movieService.MovieExists(id))
            {
                return NotFound();
            }

            if (serviceResult.ResponseError != null)
            {
                return BadRequest(serviceResult.ResponseError);
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

            if (!_movieService.CommentExists(commentId))
            {
                return NotFound();
            }

            var serviceResult = await _movieService.PutComment(commentId, _mapper.Map<Comment>(comment));

            if (serviceResult.ResponseError != null)
            {
                return BadRequest(serviceResult.ResponseError);
            }

            return NoContent();
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
            if (!_movieService.MovieExists(id))
            {
                return NotFound();
            }

            var serviceResult = await _movieService.DeleteMovie(id);

            if (serviceResult.ResponseError != null)
            {
                return BadRequest(serviceResult.ResponseError);
            }

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
            if (!_movieService.CommentExists(commentId))
            {
                return NotFound();
            }

            var serviceResult = await _movieService.DeleteComment(commentId);

            if (serviceResult.ResponseError != null)
            {
                return BadRequest(serviceResult.ResponseError);
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a movie.
        /// </summary>
        /// <param name="movieRequest">The movie.</param>
        /// <returns>The movie that was created.</returns>
        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieViewModel movieRequest)
        {
            var serviceResult = await _movieService.PostMovie(_mapper.Map<Movie>(movieRequest));

            if (serviceResult.ResponseError == null)
            {
                return CreatedAtAction("GetMovie", new { id = movieRequest.Id }, movieRequest);
            }

            return BadRequest();
        }

        /// <summary>
        /// Returns a movie and its comments, based on the given movie's Id.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <returns>A movie and its comments</returns>
        //https://localhost:5001/api/movies/1/comments
        [HttpGet("{id}/Comments")]
        public async Task<ActionResult<IEnumerable<MovieWithCommentsViewModel>>> GetCommentsForMovie(int id)
        {
            var serviceResult = await _movieService.GetCommentsForMovie(id);
            var movies = serviceResult.ResponseOk;

            return _mapper.Map<IEnumerable<Movie>, List<MovieWithCommentsViewModel>>(movies);
        }

        /// <summary>
        /// Adds a comment to a movie.
        /// </summary>
        /// <param name="id">The movie Id.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Ok if the comment was added, or NotFound if the movie was not found (based on Id.)</returns>
        //https://localhost:5001/api/movies/1/comments
        [HttpPost("{id}/Comments")]
        public async Task<IActionResult> PostCommentForMovie(int id, CommentViewModel comment)
        {
            var serviceResult = await _movieService.PostCommentForMovie(id, _mapper.Map<Comment>(comment));

            if (serviceResult.ResponseError != null)
            {
                return BadRequest();
            }

            return Ok();
        }


        /*
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
        */
    }
}
