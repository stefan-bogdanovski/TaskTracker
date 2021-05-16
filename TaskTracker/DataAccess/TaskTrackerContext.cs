using DataAccess.Configurations;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
	public class TaskTrackerContext : DbContext
	{
		#region DataForDatabase
		private List<Project> _projectDataList = new List<Project>
			{
				new Project
				{
					Id = 1,
					Name = "First project",
					StartDate = new DateTime(2021, 7, 7),
					CompletionDate = null,
					Status = ProjectStatus.Active,
					Priority = 100
				},
				new Project
				{
					Id = 2,
					Name = "Second project",
					StartDate = new DateTime(2020, 10, 7),
					CompletionDate = null,
					Status = ProjectStatus.Active,
					Priority = 200
				},
				new Project
				{
					Id = 3,
					Name = "Third project",
					StartDate = null,
					CompletionDate = null,
					Status = ProjectStatus.NotStarted,
					Priority = 500
				},
				new Project
				{
					Id = 4,
					Name = "Fourth project",
					StartDate = new DateTime(2018, 4, 25),
					CompletionDate = new DateTime(2019, 8, 3),
					Status = ProjectStatus.Completed,
					Priority = 50
				}

			};
		private List<Task> _taskDataList = new List<Task>()
			{
				new Task
				{
					Id = 1,
					Name = "Create filters for projects",
					Description = "Create filters for projects, user should filter projects by date or by name.",
					Priority = 1000,
					Status = TaskStatus.ToDo,
					ProjectId = 1
				},
				new Task
				{
					Id = 2,
					Name = "Create sorting functionality for projects",
					Description = "Create sorting functionality for projects, user should sort projects by date",
					Priority = 2000,
					Status = TaskStatus.Done,
					ProjectId = 1
				},
				new Task
				{
					Id = 3,
					Name = "Implement mailing system",
					Description = "Users should be able to contact administrator through form, when they fill the form, you should send the form details to chosen administrator.",
					Priority = 3000,
					Status = TaskStatus.ToDo,
					ProjectId = 2
				},
				new Task
				{
					Id = 4,
					Name = "Fix bugs in shopping cart",
					Description = "There are several bugs in our shopping cart",
					Priority = 500,
					Status = TaskStatus.InProgress,
					ProjectId = 2
				}
			};
		#endregion

		#region Properties
		public DbSet<Project> Projects { get; set; }
		public DbSet<Task> Tasks { get; set; }
		#endregion
		#region Overrides
		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.Entity is Entity e)
				{
					switch (entry.State)
					{
						case EntityState.Added:
							e.CreatedAt = DateTime.Now;
							e.ModifiedAt = null;
							e.DeletedAt = null;
							break;
						case EntityState.Modified:
							e.ModifiedAt = DateTime.Now;
							break;
					}
				}
			}
			return base.SaveChanges();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BQQNN13\SQLEXPRESS;Initial Catalog=TaskTracker;Integrated Security=True");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			

			modelBuilder.Entity<Project>().HasData(this._projectDataList);
			modelBuilder.Entity<Task>().HasData(this._taskDataList);

			modelBuilder.Entity<Project>()
				.HasQueryFilter(p => p.DeletedAt == null);

			modelBuilder.Entity<Task>()
				.HasQueryFilter(t => t.DeletedAt == null);

			modelBuilder.ApplyConfiguration(new ProjectConfiguration());
			modelBuilder.ApplyConfiguration(new TaskConfiguration());
		} 
		#endregion
	}
}
