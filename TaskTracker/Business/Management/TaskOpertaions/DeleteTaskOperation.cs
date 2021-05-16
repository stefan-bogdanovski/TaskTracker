using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management.TaskOpertaions
{
	public class DeleteTaskOperation : Operation
	{
		private readonly int _id;
		public DeleteTaskOperation(TaskTrackerContext context, int id)
			:base(context)
		{
			_id = id;
		}

		public override OperationResult Execute()
		{
			var task = _context.Tasks.Find(_id);
			if(task == null)
			{
				opResult.Errors.Add("Task not found.");
				return opResult;
			}
			task.DeletedAt = DateTime.Now;
			_context.Entry(task).State = EntityState.Modified;
			_context.SaveChanges();
			return opResult;
		}
	}
}
