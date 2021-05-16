using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
	public class Task : Entity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Priority { get; set; }
		public TaskStatus Status { get; set; }
		public int ProjectId { get; set; }

		public Project Project { get; set; }
	}


	public enum TaskStatus
	{
		ToDo,
		InProgress,
		Done
	}
}
