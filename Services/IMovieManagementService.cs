using Lab2.Errors;
using Lab2.Models;
using Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface IMovieManagementService
    {
        public Task<ServiceResponse<IEnumerable<Movie>, IEnumerable<MovieError>>> GetMovies();


        /*
         * public async Task<ActionResult<MovieViewModel>> GetMovie(int id)
         * public async Task<ActionResult<IEnumerable<MovieViewModel>>> SortByDateAdded(DateTime fromDate, DateTime toDate)
         * public ActionResult<IEnumerable<MovieWithCommentsViewModel>> GetCommentsForMovie(int id)
         * public IActionResult PostCommentForMovie(int id, CommentViewModel comment)
         * public async Task<IActionResult> PutMovie(int id, MovieViewModel movie)
         * public async Task<IActionResult> PutComment(int commentId, CommentViewModel comment)
         * public async Task<ActionResult<Movie>> PostMovie(MovieViewModel movieRequest)
         * public async Task<IActionResult> DeleteMovie(int id)
         * public async Task<IActionResult> DeleteComment(int commentId)
         * private bool MovieExists(int id)
         * private bool CommentExists(int id)
         */
    }
}
