using Lab2.Data;
using Lab2.Errors;
using Lab2.Models;
using Lab2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class MovieManagementService : IMovieManagementService
    {
        public ApplicationDbContext _context;
        public MovieManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<PaginatedResultSet<Movie>, IEnumerable<MovieError>>> GetMovies(int? page = 1, int? perPage = 10)
        {
            var movies = await _context.Movies
                .OrderBy(m => m.Id)
                .Skip((page.Value - 1) * perPage.Value)
                .Take(perPage.Value)
                .ToListAsync();

            var count = await _context.Movies.CountAsync();
            var resultSet = new PaginatedResultSet<Movie>(movies, page.Value, count, perPage.Value);

            var serviceResponse = new ServiceResponse<PaginatedResultSet<Movie>, IEnumerable<MovieError>>();
            serviceResponse.ResponseOk = resultSet;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Movie, IEnumerable<MovieError>>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            var serviceResponse = new ServiceResponse<Movie, IEnumerable<MovieError>>();
            serviceResponse.ResponseOk = movie;

            return serviceResponse;
        }

        public async Task<ServiceResponse<PaginatedResultSet<Movie>, IEnumerable<MovieError>>> SortByDateAdded(DateTime fromDate, DateTime toDate, int? page = 1, int? perPage = 10)
        {
            var movies = await _context.Movies
                .Where(m => m.DateAdded.CompareTo(fromDate) >= 0 && m.DateAdded.CompareTo(toDate) <= 0)
                .OrderByDescending(m => m.YearOfRelease)
                .Skip((page.Value - 1) * perPage.Value)
                .Take(perPage.Value)
                .ToListAsync();

            var count = await _context.Movies.CountAsync();
            var resultSet = new PaginatedResultSet<Movie>(movies, page.Value, count, perPage.Value);

            var serviceResponse = new ServiceResponse<PaginatedResultSet<Movie>, IEnumerable<MovieError>>();
            serviceResponse.ResponseOk = resultSet;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Movie, IEnumerable<MovieError>>> PutMovie(int id, Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            var serviceResponse = new ServiceResponse<Movie, IEnumerable<MovieError>>();

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = movie;
            }
            catch (DbUpdateConcurrencyException e)
            {
                var errors = new List<MovieError>();
                errors.Add(new MovieError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Comment, IEnumerable<MovieError>>> PutComment(int id, Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            var serviceResponse = new ServiceResponse<Comment, IEnumerable<MovieError>>();

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = comment;
            }
            catch (DbUpdateConcurrencyException e)
            {
                var errors = new List<MovieError>();
                errors.Add(new MovieError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool, IEnumerable<MovieError>>> DeleteMovie(int id)
        {
            var serviceResponse = new ServiceResponse<bool, IEnumerable<MovieError>>();

            try
            {
                var movie = await _context.Movies.FindAsync(id);
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = true;
            }
            catch (Exception e)
            {
                var errors = new List<MovieError>();
                errors.Add(new MovieError { Code = e.GetType().ToString(), Description = e.Message });
                serviceResponse.ResponseError = errors;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool, IEnumerable<MovieError>>> DeleteComment(int id)
        {
            var serviceResponse = new ServiceResponse<bool, IEnumerable<MovieError>>();

            try
            {
                var comment = await _context.Comments.FindAsync(id);
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = true;
            }
            catch (Exception e)
            {
                var errors = new List<MovieError>();
                errors.Add(new MovieError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Movie, IEnumerable<MovieError>>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            var serviceResponse = new ServiceResponse<Movie, IEnumerable<MovieError>>();

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = movie;
            }
            catch (Exception e)
            {
                var errors = new List<MovieError>();
                errors.Add(new MovieError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Comment, IEnumerable<MovieError>>> PostCommentForMovie(int id, Comment comment)
        {
            var movie = await _context.Movies.Where(m => m.Id == id).Include(m => m.Comments).FirstOrDefaultAsync();

            var serviceResponse = new ServiceResponse<Comment, IEnumerable<MovieError>>();

            try
            {
                movie.Comments.Add(comment);
                _context.Entry(movie).State = EntityState.Modified;
                _context.SaveChanges();
                serviceResponse.ResponseOk = comment;
            }
            catch (Exception e)
            {
                var errors = new List<MovieError>();
                errors.Add(new MovieError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<PaginatedResultSet<Comment>, IEnumerable<MovieError>>> GetCommentsForMovie(int id, int? page = 1, int? perPage = 10)
        {
            var comments = await _context.Comments
				.Where(c => c.MovieId == id)
                .OrderBy(c => c.Id)
				.Skip((page.Value - 1) * perPage.Value)
				.Take(perPage.Value)
				.ToListAsync();

			var count = await _context.Comments.Where(c => c.MovieId == id).CountAsync();
			var resultSet = new PaginatedResultSet<Comment>(comments, page.Value, count, perPage.Value);

			var serviceResponse = new ServiceResponse<PaginatedResultSet<Comment>, IEnumerable<MovieError>>();
			serviceResponse.ResponseOk = resultSet;
			
            return serviceResponse; 
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }

        public bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
    }
}
