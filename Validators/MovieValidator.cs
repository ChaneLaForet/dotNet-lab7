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

            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Hmm... Not a valid description, sorry.");
        }
    }
}
