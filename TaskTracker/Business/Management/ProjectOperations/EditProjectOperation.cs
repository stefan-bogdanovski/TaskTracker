using Business.DTO;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management.ProjectOperations
{
	public class EditProjectOperation : Operation
	{
		private readonly int _id;
		private readonly ProjectDto _projectDto;

		public EditProjectOperation(TaskTrackerContext context, int id, ProjectDto dto)
			:base (context)
		{
			_id = id;
			_projectDto = dto;
		}

		public override OperationResult Execute()
		{
			var project = _context.Projects.Find(_id);
			if (project == null)
			{
				return new OperationResult
				{
					Errors = new List<string>()
					{
						"Project not found."
					}
				};
			}
			project.Name = _projectDto.Name;
			project.StartDate = _projectDto.StartDate;
			project.CompletionDate = _projectDto.CompletionDate;
			project.Status = _projectDto.Status;
			project.Priority = _projectDto.Priority;

			_context.Entry(project).State = EntityState.Modified;
			_context.SaveChanges();
			return new OperationResult();
		}
	}
}
