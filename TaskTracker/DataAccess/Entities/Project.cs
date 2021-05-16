using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
	public class Project : Entity
	{
		public string Name { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? CompletionDate { get; set; }
		public ProjectStatus Status { get; set; }
		public int Priority { get; set; }

		public ICollection<Task> Tasks { get; set; }
	}
	public enum ProjectStatus
	{
		NotStarted,
		Active,
		Completed
	}
}
