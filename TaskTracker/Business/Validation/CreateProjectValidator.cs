using Business.DTO;
using DataAccess;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Validation
{
    public class CreateProjectValidator : AbstractValidator<ProjectDto>
    {
        private readonly TaskTrackerContext _context;

        public CreateProjectValidator(TaskTrackerContext context)
        {
            this._context = context;

            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Name is a required parameter.")
                .Must(name => !_context.Projects.Any(p => p.Name == name))
                .WithMessage(p => $"This project name {p.Name} already exists.");

            RuleFor(p => p.Name)
                .MaximumLength(80)
                .WithMessage("Maximum project length is 80 characters.");

            RuleFor(p => p.StartDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"Starting date must be before {DateTime.Now}.");

            When(p => p.CompletionDate != null, () => RuleFor(p => p.StartDate)
                .LessThan(p => p.CompletionDate)
                .WithMessage(p => $"Starting date must be before ({p.CompletionDate}) completion date."));

            When(p => p.StartDate == null, () => RuleFor(p => p.CompletionDate)
            .Null()
            .WithMessage("Completion date cannot be provided if project has no starting date."));

            RuleFor(p => p.CompletionDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"Completion date must be before {DateTime.Now}.");

            RuleFor(p => p.Priority)
                .NotEmpty()
                .WithMessage("Priority is required parameter.");

            RuleFor(p => p.Status)
                .IsInEnum();
        }

    }
}
