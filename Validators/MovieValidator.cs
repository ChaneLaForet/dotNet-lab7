using FluentValidation;
using Lab2.Data;
using Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Validators
{
    public class MovieValidator : AbstractValidator<MovieViewModel>
    {
        private readonly ApplicationDbContext _context;
        public MovieValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(m => m.Title).NotNull();
            RuleFor(m => m.Description).MinimumLength(20);
            RuleFor(m => m.Genre).NotNull();
            RuleFor(m => m.DurationInMinutes).InclusiveBetween(1, 1500);
            RuleFor(m => m.YearOfRelease).InclusiveBetween(1800, DateTime.Now.Year);
            RuleFor(m => m.Director).NotNull();
            RuleFor(m => m.Rating).InclusiveBetween(1, 10).When(m => m.Watched == true);
        }
    }
}
