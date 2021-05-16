using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
	public class TaskConfiguration : IEntityTypeConfiguration<Task>
	{
		public void Configure(EntityTypeBuilder<Task> builder)
		{
			builder.HasKey(t => t.Id);

			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(80);

			builder.Property(t => t.Status)
				.IsRequired();

			builder.Property(t => t.Priority)
				.IsRequired();
		}
	}
}
