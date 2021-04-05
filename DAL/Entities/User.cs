using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	public class User : IdentityUser
	{
		[Required(ErrorMessage = "FirstName is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for FirstName is 60 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for LastName is 60 characters.")]
		public string LastName { get; set; }
	}
}
