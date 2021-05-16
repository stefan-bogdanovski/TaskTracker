using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
	class ProjectConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.HasKey(p => p.Id);

			builder.HasIndex(p => p.Name).IsUnique();
			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(80);

			builder.Property(p => p.Priority)
				.IsRequired();

			builder.Property(p => p.Status)
				.IsRequired();

			builder.HasMany(p => p.Tasks)
				.WithOne(t => t.Project)
				.HasForeignKey(t => t.ProjectId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
