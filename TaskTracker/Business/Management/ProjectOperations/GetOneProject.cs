using Business.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Business.Management.ProjectOperations
{
	public class GetOneProject : Operation
	{
		private int _projectId;
		public GetOneProject(TaskTrackerContext _context, int projectId)
			:base (_context)
		{
			_projectId = projectId;
		}
		public override OperationResult Execute()
		{
			var project = _context.Projects.Find(this._projectId);
			if(project == null)
			{
				return new OperationResult
				{
					Errors = new List<string>()
					{
						"Project not found."
					}
				};
			}
			ProjectDto Dto = new ProjectDto
			{
				Name = project.Name,
				Status = project.Status,
				StartDate = project.StartDate,
				CompletionDate = project.CompletionDate,
				Priority = project.Priority
			};
			var list = new List<ProjectDto>();
			list.Add(Dto);
			this.opResult.Data = list;
			return this.opResult;
		}
	}
}
