using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
	/// <summary>
	/// Controller for working with statuses.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class StatusesController : ControllerBase
	{
		private readonly IStatusService _statusService;

		/// <summary>
		/// Constructor that gets <see cref="IStatisticService"/>.
		/// </summary>
		/// <param name="statusService"></param>
		public StatusesController(IStatusService statusService)
		{
			_statusService = statusService;
		}

		/// <summary>
		/// Creates new status, only Administrator has acces to this action.
		/// </summary>
		/// <param name="createStatusDto"></param>
		/// <returns></returns>
		[HttpPost, Authorize(Roles = "Administrator")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<ReadStatusDto>> Add([FromBody] CreateStatusDto createStatusDto)
		{
			try
			{
				var addedStatus = await _statusService.AddAsync(createStatusDto);
				return CreatedAtAction("Add", addedStatus);
			}
			catch (TaskException ex)
			{
				return Conflict(ex.Message);
			}
		}

		/// <summary>
		/// Gets all statuses.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ReadStatusDto>>> Get()
		{
			var statuses = await _statusService.GetAllAsync();
			return Ok(statuses);
		}

		/// <summary>
		/// Deletes status with id = <paramref name="statusId"/>, only Administrator has acces to this action.
		/// </summary>
		/// <param name="statusId"></param>
		/// <returns></returns>
		[HttpDelete("{statusId}"), Authorize(Roles = "Administrator")]
		public async Task<ActionResult<ReadStatusDto>> Delete(int statusId)
		{
			try
			{
				var deletedStatus = await _statusService.DeleteByIdAsync(statusId);
				return Ok(deletedStatus);
			}
			catch (TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}

		/// <summary>
		/// Updates status with id = <paramref name="statusId"/>, only Administrator has acces to this action.
		/// </summary>
		/// <param name="statusId"></param>
		/// <param name="createStatusDto"></param>
		/// <returns></returns>
		[HttpPut("{statusId}"), Authorize(Roles = "Administrator")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<ActionResult<ReadStatusDto>> Update(int statusId, [FromBody] CreateStatusDto createStatusDto)
		{
			try
			{
				var updatedProject = await _statusService.UpdateAsync(statusId, createStatusDto);
				return Ok(updatedProject);
			}
			catch (TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
