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
    }
}
