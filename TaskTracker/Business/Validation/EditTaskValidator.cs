using Business.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Validation
{
	public class EditTaskValidator : AbstractValidator<TaskDto>
	{
		private readonly TaskTrackerContext _context;
		private readonly int _id;
		public EditTaskValidator(TaskTrackerContext context, int id)
		{
			_context = context;
			_id = id;

			RuleFor(t => t.Name)
				.NotEmpty().WithMessage("Name property should not be empty.")
				.NotNull().WithMessage("You are missing name property.")
				.MaximumLength(80).WithMessage("Name should not be longer than 80 characters.")
				.Must(t => !_context.Tasks.Any(task => task.Name == t && task.Id != _id));
			//We should not allow tasks with the same name so we avoid duplication, but we allow leaving the old name during editing.

			RuleFor(t => t.Description)
				.NotEmpty().WithMessage("Description property should not be empty.")
				.NotNull().WithMessage("You are missing description property.");

			RuleFor(t => t.Status)
				.IsInEnum().WithMessage("Invalid status value.");

			RuleFor(t => t.Priority)
				.NotEmpty().WithMessage("Priority property should not be empty.")
				.NotNull().WithMessage("You are missing priority property.");

			RuleFor(t => t.ProjectId).Must(p => _context.Projects.Any(proj => proj.Id == p)).WithMessage("Project with given Id does not exist.");
		}
	}
}
