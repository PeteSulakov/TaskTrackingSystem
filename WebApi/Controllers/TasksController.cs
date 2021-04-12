using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
	/// <summary>
	/// Controller for working with tasks.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly ITaskService _taskService;
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;

		/// <summary>
		/// Constructor that gets <see cref="ITaskService"/>, <see cref="IEmailService"/> and <see cref="UserManager{TUser}"/>.
		/// </summary>
		/// <param name="taskService"></param>
		/// <param name="emailService"></param>
		/// <param name="userManager"></param>
		public TasksController(ITaskService taskService, IEmailService emailService, UserManager<User> userManager)
		{
			_taskService = taskService;
			_emailService = emailService;
			_userManager = userManager;
		}

		/// <summary>
		/// Gets tasks for current developer.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = "Developer")]
		public async Task<ActionResult<IEnumerable<ReadProjectDto>>> Get()
		{
			var developerId = _userManager.GetUserId(User);
			var tasks = await _taskService.GetTasksByDeveloperIdAsync(developerId);
			if (!tasks.Any())
				return NotFound("Can't find any tasks.");
			return Ok(tasks);
		}

		/// <summary>
		/// Gets task by id for current developer.
		/// </summary>
		/// <param name="taskId"></param>
		/// <returns></returns>
		[HttpGet("{taskId}")]
		[Authorize(Roles = "Developer")]
		public async Task<ActionResult<ReadTaskDto>> GetById(int taskId)
		{
			var developerId = _userManager.GetUserId(User);
			try
			{
				var task = await _taskService.GetByIdAsync(taskId, developerId);
				return Ok(task);
			}
			catch (TaskException ex)
			{
				return ex.StatusCode switch
				{
					HttpStatusCode.Forbidden => StatusCode(403, ex.Message),
					_ => NotFound(ex.Message)
				};
			}
		}

		/// <summary>
		/// Creates new task, only manager has access to this action.
		/// </summary>
		/// <param name="taskDto"></param>
		/// <returns></returns>
		[HttpPost, Authorize(Roles = "Manager")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<ReadTaskDto>> Add([FromBody] CreateTaskDto taskDto)
		{
			try
			{
				var managerId = _userManager.GetUserId(User);
				var addedTask = await _taskService.AddAsync(taskDto, managerId);
				await _emailService.SendEmailAsync(taskDto.DeveloperEmail, taskDto.Title, taskDto.Description);
				return CreatedAtAction(nameof(Add), addedTask);
			}
			catch(TaskException ex)
			{
				return ex.StatusCode switch
				{
					HttpStatusCode.Forbidden => StatusCode(403, ex.Message),
					_ => NotFound(ex.Message)
				};
			}
		}

		/// <summary>
		/// Deletes task by id, only manager has access to this action.
		/// </summary>
		/// <param name="taskId"></param>
		/// <returns></returns>
		[HttpDelete("{taskId}"), Authorize(Roles = "Manager")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult> Delete(int taskId)
		{
			var managerId = _userManager.GetUserId(User);
			try
			{
				var deletedTask = await _taskService.DeleteByIdAsync(taskId, managerId);
				return Ok(deletedTask);
			}
			catch(TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}

		/// <summary>
		/// Updates task with id = <paramref name="taskId"/>.
		/// </summary>
		/// <param name="taskId"></param>
		/// <param name="updateTaskDto"></param>
		/// <returns></returns>
		[HttpPut("{taskId}")]
		public async Task<ActionResult<ReadProjectDto>> Update(int taskId, [FromBody] UpdateTaskDto  updateTaskDto)
		{
			var managerId = _userManager.GetUserId(User);
			try
			{
				var updatedTask = await _taskService.UpdateAsync(taskId, managerId, updateTaskDto);
				return Ok(updatedTask);
			}
			catch (TaskException ex)
			{
				return ex.StatusCode switch
				{
					HttpStatusCode.Forbidden => StatusCode(403, ex.Message),
					_ => NotFound(ex.Message)
				};
			}
		}

		/// <summary>
		/// Updates task status, only developer has access to this action.
		/// </summary>
		/// <param name="taskId"></param>
		/// <param name="statusId"></param>
		/// <returns></returns>
		[HttpPut("{taskId}/status/{statusId}")]
		public async Task<ActionResult<ReadProjectDto>> UpdateTaskStatus(int taskId, int statusId)
		{
			var developerId = _userManager.GetUserId(User);
			try
			{
				var updatedTask = await _taskService.UpdateTaskStatus(taskId, statusId, developerId);
				return Ok(updatedTask);
			}
			catch (TaskException ex)
			{
				return ex.StatusCode switch
				{
					HttpStatusCode.Forbidden => StatusCode(403, ex.Message),
					_ => NotFound(ex.Message)
				};
			}

		}

	}
}
