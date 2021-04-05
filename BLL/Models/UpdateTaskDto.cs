using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
	public class UpdateTaskDto
	{
		[Required(ErrorMessage = "Title of task is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for Title is 60 characters.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Description of task is a required field.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "IssueDate of task is a required field.")]
		public DateTime IssueDate { get; set; }

		[Required(ErrorMessage = "DeadLine of task is a required field.")]
		public DateTime DeadLine { get; set; }
		public string DeveloperEmail { get; set; }
		public int StatusId { get; set; }
	}
}
