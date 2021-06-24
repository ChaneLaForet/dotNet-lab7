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

        public async Task<ServiceResponse<IEnumerable<Movie>, IEnumerable<MovieError>>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            var serviceResponse = new ServiceResponse<IEnumerable<Movie>, IEnumerable<MovieError>>();
            serviceResponse.ResponseOk = movies;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Movie, IEnumerable<MovieError>>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            var serviceResponse = new ServiceResponse<Movie, IEnumerable<MovieError>>();
            serviceResponse.ResponseOk = movie;

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Movie>, IEnumerable<MovieError>>> SortByDateAdded(DateTime fromDate, DateTime toDate)
        {
            var movies = await _context.Movies.Where(m => m.DateAdded.CompareTo(fromDate) >= 0 && m.DateAdded.CompareTo(toDate) <= 0)
                                             .OrderByDescending(m => m.YearOfRelease).ToListAsync();

            var serviceResponse = new ServiceResponse<IEnumerable<Movie>, IEnumerable<MovieError>>();
            serviceResponse.ResponseOk = movies;
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
