using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configuration
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData
			(
				new IdentityRole
				{
					Id = "0f15c6f7-b0eb-4d47-b306-f2bf7c0d068d",
					Name = "Developer",
					NormalizedName = "DEVELOPER"
				},
				new IdentityRole
				{
					Id = "8b33a46d-a182-4dc4-ac38-1fa2c917b9be",
					Name = "Manager",
					NormalizedName = "MANAGER"
				},
				new IdentityRole
				{
					Id = "f4fafec6-f6ac-4b2d-aae7-bc4c20bb46bb",
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				}
			);
		}
	}
}
