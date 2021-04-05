using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ActionFilters;
using WebApi.Infrastructure;

namespace WebApi.Controllers
{
	/// <summary>
	/// Controller for authenticaton.
	/// </summary>
	[Route("api/authentication")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly ILoggerManager _logger;
		private readonly IAuthenticationService _authenticationService;

		/// <summary>
		/// Constructor that gets <see cref="ILoggerManager"/> and <see cref="IAuthenticationService"/>.
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="authenticationService"></param>
		public AuthenticationController(ILoggerManager logger, IAuthenticationService authenticationService)
		{
			_logger = logger;
			_authenticationService = authenticationService;
		}

		/// <summary>
		/// Creates new user.
		/// </summary>
		/// <param name="userForRegistration"></param>
		/// <returns></returns>
		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
		{
			var result = await _authenticationService.AddUserAsync(userForRegistration);

			if (!result.Succeeded)
			{
				foreach(var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}
			return StatusCode(201);
		}

		/// <summary>
		/// Authenticates user in system.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[HttpPost("login")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
		{
			if (!await _authenticationService.ValidateUser(user))
			{
				_logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
				return Unauthorized();
			}
			return Ok(new { Token = await _authenticationService.CreateToken() });
		}
	}
}
