using Business.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Validation
{
	public class CreateTaskValidator : AbstractValidator<TaskDto>
	{
		private readonly TaskTrackerContext _context;
		public CreateTaskValidator(TaskTrackerContext context)
		{
			_context = context;

			RuleFor(t => t.Name)
				.NotEmpty().WithMessage("Name property should not be empty.")
				.NotNull().WithMessage("You are missing name property.")
				.MaximumLength(80).WithMessage("Name should not be longer than 80 characters.");

			RuleFor(t => t.Description)
				.NotEmpty().WithMessage("Description property should not be empty.")
				.NotNull().WithMessage("You are missing description property.");

			RuleFor(t => t.Status)
				.IsInEnum().WithMessage("Invalid status value.");

			RuleFor(t => t.Priority)
				.NotEmpty().WithMessage("Priority property should not be empty.")
				.NotNull().WithMessage("You are missing priority property.");

			RuleFor(t => t.ProjectId).Must(p => _context.Projects.Any(proj => proj.Id == p )).WithMessage("Project with given Id does not exist.");
		}
	}
}
