using BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IAuthenticationService 
	{
		Task<IdentityResult> AddUserAsync(UserForRegistrationDto userForRegistration);
		Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
		Task<string> CreateToken();
	}
}
