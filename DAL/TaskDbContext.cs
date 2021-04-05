using DAL.Configuration;
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
	public class TaskDbContext : IdentityDbContext<User>
	{
		public TaskDbContext(DbContextOptions options)
			: base(options)
		{ }

		public DbSet<Task> Tasks { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Status> Statuses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new StatusConfiguration());
			builder.ApplyConfiguration(new RoleConfiguration());
		}
	}
}
