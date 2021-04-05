using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
	/// <summary>
	/// Controller for projects only the "Manager" has access.
	/// </summary>
	[Route("api/[controller]")]
	[Authorize(Roles = "Manager")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly IProjectService _projectService;
		private readonly UserManager<User> _userManager;


		/// <summary>
		/// Constructor that gets <see cref="IProjectService"/> and <see cref="UserManager{User}"/>.
		/// </summary>
		/// <param name="projectService"></param>
		/// <param name="userManager"></param>
		public ProjectsController(IProjectService projectService, UserManager<User> userManager)
		{
			_projectService = projectService;
			_userManager = userManager;
		}

		/// <summary>
		/// Gets all projects that were created by the current manager.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ReadProjectDto>>> Get()
		{
			var managerId = _userManager.GetUserId(User);
			var projects = await _projectService.GetProjectsByManagerIdAsync(managerId);
			if (projects == null)
				return NotFound("Can't find any projects");
			return Ok(projects);
		}

		/// <summary>
		/// Gets project by id that was created by current manager.
		/// </summary>
		/// <param name="projectId"></param>
		/// <returns></returns>
		[HttpGet("{projectId}")]
		public async Task<ActionResult<ReadProjectDto>> GetById(int projectId)
		{
			var managerId = _userManager.GetUserId(User);
			try
			{
				var project = await _projectService.GetByIdAsync(projectId, managerId);
				return Ok(project);
			}
			catch (TaskException ex)
			{
				return ex.StatusCode switch
				{
					HttpStatusCode.Forbidden => Forbid(ex.Message),
					_ => NotFound(ex.Message)
				};
			}
		}

		/// <summary>
		/// Gets task with project id = <paramref name="projectId"/> and task id = <paramref name="taskId"/>.
		/// </summary>
		/// <param name="projectId"></param>
		/// <param name="taskId"></param>
		/// <returns></returns>
		[HttpGet("{projectId}/tasks/{taskId}")]
		public async Task<ActionResult<ReadTaskDto>> GetTaskById(int projectId, int taskId)
		{
			var managerId = _userManager.GetUserId(User);
			try
			{
				var task = await _projectService.GetTaskInProjectAsync(projectId, taskId, managerId);
				return Ok(task);
			}
			catch(TaskException ex)
			{
				return ex.StatusCode switch
				{
					HttpStatusCode.Forbidden => Forbid(ex.Message),
					_ => NotFound(ex.Message)
				};
			}
		}


		/// <summary>
		/// Creates new project.
		/// </summary>
		/// <param name="projectDto"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<ReadProjectDto>> Add([FromBody] CreateProjectDto projectDto)
		{
			var managerId = _userManager.GetUserId(User);
			projectDto.ManagerId = managerId;
			return Ok(await _projectService.AddAsync(projectDto));
		}

		/// <summary>
		/// Updates project info.
		/// </summary>
		/// <param name="projectId"></param>
		/// <param name="updateProjectDto"></param>
		/// <returns></returns>
		[HttpPut("{projectId}")]
		public async Task<ActionResult<ReadProjectDto>> Update(int projectId, [FromBody] UpdateProjectDto updateProjectDto)
		{
			var managerId = _userManager.GetUserId(User);
			try
			{
				var updatedProject = await _projectService.UpdateAsync(projectId, managerId, updateProjectDto);
				return Ok(updatedProject);
			}
			catch(TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}

		/// <summary>
		/// Deletes project.
		/// </summary>
		/// <param name="projectId"></param>
		/// <returns></returns>
		[HttpDelete("{projectId}")]
		public async Task<ActionResult> Delete(int projectId)
		{
			var managerId = _userManager.GetUserId(User);
			try
			{
				var deletedProject = await _projectService.DeleteByIdAsync(projectId, managerId);
				return Ok(deletedProject);

			}
			catch (TaskException ex)
			{
				return ex.StatusCode switch
				{
					HttpStatusCode.Forbidden => Forbid(ex.Message),
					_ => NotFound(ex.Message)
				};
			}
		}
	}
}
