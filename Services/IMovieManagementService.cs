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
        public Task<ServiceResponse<Movie, IEnumerable<MovieError>>> GetMovie(int id);
        public Task<ServiceResponse<IEnumerable<Movie>, IEnumerable<MovieError>>> SortByDateAdded(DateTime fromDate, DateTime toDate);
        public Task<ServiceResponse<Movie, IEnumerable<MovieError>>> PutMovie(int id, Movie movie);
        public Task<ServiceResponse<bool, IEnumerable<MovieError>>> DeleteMovie(int id);
        public bool MovieExists(int id);
        public bool CommentExists(int id);

        /*
         * public ActionResult<IEnumerable<MovieWithCommentsViewModel>> GetCommentsForMovie(int id)
         * public IActionResult PostCommentForMovie(int id, CommentViewModel comment)
         * public async Task<IActionResult> PutComment(int commentId, CommentViewModel comment)
         * public async Task<ActionResult<Movie>> PostMovie(MovieViewModel movieRequest)
         * public async Task<IActionResult> DeleteMovie(int id)
         * public async Task<IActionResult> DeleteComment(int commentId)
         */
    }
}
