using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management.ProjectOperations
{
	public class DeleteProjectOperation : Operation
	{
		private readonly int _id;

		public DeleteProjectOperation(TaskTrackerContext _context, int id)
			:base (_context)
		{
			_id = id;
		}
		public override OperationResult Execute()
		{
			var project = _context.Projects.Find(_id);
			if(project == null)
			{
				return new OperationResult()
				{
					Errors = new List<string>()
					{
						"Entity not found."
					}
				};
			}
			project.DeletedAt = DateTime.Now;
			_context.Entry(project).State = EntityState.Modified;
			_context.SaveChanges();
			return new OperationResult();
		}
	}
}
