using Business.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Management.ProjectOperations
{
	public class GetAllProjectsOperaton : Operation
	{
		private readonly ProjectFilterDto _filterDto;
		public GetAllProjectsOperaton(TaskTrackerContext context, ProjectFilterDto filterDto)
			:base(context)
		{
			_filterDto = filterDto;
		}

		public override OperationResult Execute()
		{
			var projects = _context.Projects.AsQueryable();
			if(!string.IsNullOrEmpty(_filterDto.Name))
			{
				projects = projects.Where(p => p.Name.ToLower().Contains(_filterDto.Name.ToLower()));
			}
			if(_filterDto.StartDate != null)
			{
				projects = projects.Where(p => p.StartDate >= _filterDto.StartDate);
			}
			if(_filterDto.CompletionDate != null)
			{
				projects = projects.Where(p => p.CompletionDate <= _filterDto.CompletionDate);
			}
			if(_filterDto.Status != null)
			{
				projects = projects.Where(p => p.Status == _filterDto.Status);
			}
			if(_filterDto.Priority != null)
			{
				projects = projects.Where(p => p.Priority <= _filterDto.Priority);
			}
			var data = projects.OrderBy(p => p.Priority).Select(p => new ProjectDto
			{
				Name = p.Name,
				Status = p.Status,
				StartDate = p.StartDate,
				CompletionDate = p.CompletionDate
			}).ToList();
			this.opResult.Data = data;
			return this.opResult;		
		}
	}
}
