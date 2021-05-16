using Business.DTO;
using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management.ProjectOperations
{
	public class AddProjectOperation : Operation
	{
		private readonly ProjectDto _projectDto;

		public AddProjectOperation(TaskTrackerContext _context, ProjectDto projectDto)
			:base (_context)
		{
			this._projectDto = projectDto;
		}

		public override OperationResult Execute()
		{
			var project = new Project
			{
				Name = _projectDto.Name,
				StartDate = _projectDto.StartDate,
				CompletionDate = _projectDto.CompletionDate,
				Priority = _projectDto.Priority,
				Status = _projectDto.Status
			};
			_context.Projects.Add(project);
			_context.SaveChanges();
			return new OperationResult();
		}
	}
}
