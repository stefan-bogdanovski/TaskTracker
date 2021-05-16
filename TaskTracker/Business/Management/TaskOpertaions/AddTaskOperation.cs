using Business.DTO;
using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management.TaskOpertaions
{
	public class AddTaskOperation : Operation
	{
		private readonly TaskDto _dto;
		public AddTaskOperation(TaskTrackerContext context, TaskDto dto)
			:base (context)
		{
			_dto = dto;
		}
		public override OperationResult Execute()
		{
			Task t = new Task();
			t.Name = _dto.Name;
			t.Description = _dto.Description;
			t.Priority = _dto.Priority;
			t.Status = _dto.Status;
			t.ProjectId = _dto.ProjectId;
			_context.Tasks.Add(t);
			_context.SaveChanges();
			return opResult;
		}
	}
}
