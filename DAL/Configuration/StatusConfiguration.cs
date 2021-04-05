using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configuration
{
	public class StatusConfiguration : IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			builder.HasData
			(
				new Status
				{
					Id = 1,
					Title = "Open"
				},
				new Status
				{
					Id = 2,
					Title = "Started"
				},
				new Status
				{
					Id = 3,
					Title = "Closed"
				}
			);
		}
	}
}
