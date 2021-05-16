using Business.DTO;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management.TaskOpertaions
{
	public class EditTaskOperation : Operation
	{
		private readonly TaskDto _dto;
		private readonly int _id;
		public EditTaskOperation(TaskTrackerContext context, TaskDto dto, int id)
			:base(context)
		{
			_dto = dto;
			_id = id;
		}

		public override OperationResult Execute()
		{
			var task = _context.Tasks.Find(_id);
			if(task == null)
			{
				opResult.Errors.Add("Task to edit not found.");
				return opResult;
			}
			task.Name = _dto.Name;
			task.Description = _dto.Description;
			task.Status = _dto.Status;
			task.Priority = _dto.Priority;
			task.ProjectId = _dto.ProjectId;
			_context.Entry(task).State = EntityState.Modified;
			_context.SaveChanges();
			return opResult;
		}
	}
}
