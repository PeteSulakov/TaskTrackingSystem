using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	/// <summary>
	/// Controller for Administrator.
	/// </summary>
	[Route("api/[controller]")]
	[Authorize(Roles = "Administrator")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;

		/// <summary>
		/// Constructor that gets <see cref="IAdminService"/> service.
		/// </summary>
		/// <param name="adminService"></param>
		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}


		/// <summary>
		/// Gets all users with a specific role.
		/// </summary>
		/// <param name="userRole"></param>
		/// <returns></returns>
		[HttpGet("{userRole}")]
		public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole(string userRole)
		{
			try
			{
				return Ok(await _adminService.GetUsersByRole(userRole));
			}
			catch(TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}
		
		/// <summary>
		/// Updates user roles.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		[HttpPut("{userName}/{roleName}")]
		public async Task<ActionResult<User>> UpdateUserRole(string userName, string roleName)
		{
			try
			{
				return Ok(await _adminService.UpdateUserRole(userName, roleName));
			}
			catch (TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}

		/// <summary>
		/// Deletes user role.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		[HttpDelete("{userName}/{roleName}")]
		public async Task<ActionResult<User>> DeleteUserRole(string userName, string roleName)
		{
			try
			{
				return Ok(await _adminService.DeleteUserRole(userName, roleName));
			}
			catch (TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
