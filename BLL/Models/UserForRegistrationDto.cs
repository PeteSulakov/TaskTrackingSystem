using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
	public class UserForRegistrationDto
	{
		[Required(ErrorMessage = "FirstName is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for FirstName is 60 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for LastName is 60 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
		public string PhoneNumber { get; set; }

	}
}
