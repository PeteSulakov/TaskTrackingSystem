using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
	public class ReadTaskDto
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime IssueDate { get; set; }

		public DateTime DeadLine { get; set; }

		public DateTime CompletionDate { get; set; }

		public int ProjectId { get; set; }

		public string DeveloperEmail { get; set; }

		public int StatusId { get; set; }
	}
}
