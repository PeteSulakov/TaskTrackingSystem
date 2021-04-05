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
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	/// <summary>
	/// Controller for viewing project statistics.
	/// </summary>
	[Route("api/[controller]")]
	[Authorize(Roles = "Manager")]
	[ApiController]
	public class StatisticsController : ControllerBase
	{
		private readonly IStatisticService _statisticService;
		private readonly UserManager<User> _userManager;

		/// <summary>
		/// Constructor that gets <see cref="IStatisticService"/> and <see cref="UserManager{User}"/>.
		/// </summary>
		/// <param name="statisticService"></param>
		/// <param name="userManager"></param>
		public StatisticsController(IStatisticService statisticService, UserManager<User> userManager)
		{
			_statisticService = statisticService;
			_userManager = userManager;
		}

		/// <summary>
		/// Gets project with id = <paramref name="id"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("projects/{id}")]
		public async Task<ActionResult<ProjectStatisticDto>> Get(int id)
		{
			var managerId = _userManager.GetUserId(User);
			try
			{
				var projectStatistic = await _statisticService.GetProjectStatisticAsync(id, managerId);
				return projectStatistic;
			}
			catch (TaskException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
