using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
	public class UpdateProjectDto
	{
		[MaxLength(60, ErrorMessage = "Maximum length for Title is 60 characters.")]
		public string Title { get; set; }
		public DateTime EndDate { get; set; }
	}
}
