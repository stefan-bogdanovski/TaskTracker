using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTO
{
	public class TaskFilterDto
	{
		public string Name { get; set; }

		public TaskStatus? Status { get; set; }

		public string Description { get; set; }

		public int? Priority { get; set; }
	}
}
