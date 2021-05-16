using Business.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Business.Management.TaskOpertaions
{
	public class GetOneTaskOperation : Operation
	{
		private readonly int _id;
		public GetOneTaskOperation(TaskTrackerContext context, int id)
			:base(context)
		{
			_id = id;
		}
		public override OperationResult Execute()
		{
			var dto = _context.Tasks.Where(t => t.Id == _id).Select(t => new TaskDto 
			{
				Name = t.Name,
				Description = t.Description,
				Status = t.Status,
				ProjectId = t.ProjectId,
				Priority = t.Priority,
				ProjectName = t.Project.Name
			}).ToList();
			if(dto == null || dto.Count == 0)
			{
				return new OperationResult
				{
					Errors = new List<string>
					{
						"Task not found."
					}
				};
			}
			opResult.Data = dto;
			return opResult;
		}
	}
}
