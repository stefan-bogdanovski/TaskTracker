using Business.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Management.TaskOpertaions
{
	public class GetAllTasksOperation : Operation
	{
		private readonly TaskFilterDto _taskFilterDto;
		public GetAllTasksOperation(TaskTrackerContext context, TaskFilterDto taskFilterDto)
			:base (context)
		{
			_taskFilterDto = taskFilterDto;
		}
		public override OperationResult Execute()
		{
			var tasks = _context.Tasks.AsQueryable();
			if(!string.IsNullOrEmpty(_taskFilterDto.Name))
			{
				tasks = tasks.Where(t => t.Name.ToLower().Contains(_taskFilterDto.Name.ToLower()));
			}
			if(!string.IsNullOrEmpty(_taskFilterDto.Description))
			{
				tasks = tasks.Where(t => t.Description.ToLower().Contains(_taskFilterDto.Description.ToLower()));
			}
			if(_taskFilterDto.Status != null)
			{
				tasks = tasks.Where(t => t.Status == _taskFilterDto.Status);
			}
			if(_taskFilterDto.Priority != null)
			{
				tasks = tasks.Where(t => t.Priority <= _taskFilterDto.Priority);
			}
			var result = tasks.OrderBy(t => t.Priority).Select(t => new TaskDto
			{
				Name = t.Name,
				Description = t.Description,
				Status = t.Status,
				Priority = t.Priority,
				ProjectId = t.Project.Id,
				ProjectName = t.Project.Name
			}).ToList();
			opResult.Data = result;
			return opResult;
		}
	}
}
