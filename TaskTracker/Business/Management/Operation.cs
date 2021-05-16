using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Management
{
	public abstract class Operation
	{
		protected TaskTrackerContext _context;

		protected OperationResult opResult { get; set; }

		public abstract OperationResult Execute();

		public Operation(TaskTrackerContext context)
		{
			_context = context;
			this.opResult = new OperationResult();
		}
	}
}
