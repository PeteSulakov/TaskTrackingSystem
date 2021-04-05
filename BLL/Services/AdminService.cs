using BLL.Interfaces;
using BLL.Validation;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AdminService : IAdminService
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AdminService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<User> DeleteUserRole(string userName, string roleName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
				throw new TaskException($"User \"{userName}\" not found.", HttpStatusCode.NotFound);
			if (!await _roleManager.RoleExistsAsync(roleName))
				throw new TaskException($"Role \"{roleName}\" not found.", HttpStatusCode.NotFound);

			await _userManager.RemoveFromRoleAsync(user, roleName);
			return user;
		}

		public async Task<IEnumerable<User>> GetUsersByRole(string roleName)
		{
			if (!await _roleManager.RoleExistsAsync(roleName))
				throw new TaskException($"Role \"{roleName}\" not found.", HttpStatusCode.NotFound);
			return await _userManager.GetUsersInRoleAsync(roleName);
		}

		public async Task<User> UpdateUserRole(string userName, string roleName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
				throw new TaskException($"User \"{userName}\" not found.", HttpStatusCode.NotFound);
			if (!await _roleManager.RoleExistsAsync(roleName))
				throw new TaskException($"Role \"{roleName}\" not found.", HttpStatusCode.NotFound);

			await _userManager.AddToRoleAsync(user, roleName);
			return user;
		}
	}
}
