using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTO
{
	public class ProjectFilterDto
	{
		public string Name { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? CompletionDate { get; set; }
		public ProjectStatus? Status { get; set; }
		public int? Priority { get; set; }
	}
}
