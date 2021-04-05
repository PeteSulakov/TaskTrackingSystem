using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
	public class CreateProjectDto
	{
		[Required(ErrorMessage = "Title of task is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for Title is 60 characters.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "StartDate of project is a required field.")]
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string ManagerId { get; set; }
	}
}
