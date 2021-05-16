using Business.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Management.ProjectOperations
{
	public class GetAllTasksForOneProject : Operation
	{
		private readonly int _projectId;
		public GetAllTasksForOneProject(TaskTrackerContext context, int projectId)
			:base(context)
		{
			_projectId = projectId;
		}

		public override OperationResult Execute()
		{
			var project = _context.Projects.Find(_projectId);
			if(project == null)
			{
				opResult.Errors.Add("Project does not exist");
				return opResult;
			}
			var tasks = _context.Tasks.Where(t => t.ProjectId == _projectId).OrderBy(t => t.Priority).Select(t => new TaskDto
			{
				Name = t.Name,
				Description = t.Description,
				Status = t.Status,
				Priority = t.Priority,
				ProjectName = t.Project.Name,
				ProjectId = t.ProjectId
			});
			opResult.Data = tasks;
			return opResult;
		}
	}
}
