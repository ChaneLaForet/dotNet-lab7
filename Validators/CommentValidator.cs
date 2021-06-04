using FluentValidation;
using Lab2.Data;
using Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Validators
{
    public class CommentValidator : AbstractValidator<CommentViewModel>
    {
        private readonly ApplicationDbContext _context;
        public CommentValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Content).NotNull();
            RuleFor(c => c.Content).MaximumLength(150);
            RuleFor(c => c.Rating).InclusiveBetween(1, 10);
            RuleFor(c => c.DateTime).LessThan(DateTime.Now);
        }
    }
}
