using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	public class Task
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title of task is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for Title is 60 characters.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Description of task is a required field.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "IssueDate of task is a required field.")]
		public DateTime IssueDate { get; set; }

		[Required(ErrorMessage = "DeadLine of task is a required field.")]
		public DateTime DeadLine { get; set; }

		public DateTime CompletionDate { get; set; }

		public int ProjectId { get; set; }

		public Project Project { get; set; }

		public string DeveloperId { get; set; }
		
		public User Developer { get; set; }

		public int StatusId { get; set; }

		public Status Status { get; set; } 
	}
}
