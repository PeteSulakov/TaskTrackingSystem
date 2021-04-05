using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
	public class Project
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title of task is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for Title is 60 characters.")]
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string ManagerId { get; set; }
		public User Manager { get; set; }
		public ICollection<Task> Tasks { get; set; }
	}
}
