using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
	public class ReadProjectDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string ManagerId { get; set; }
		public ICollection<int> TasksIds { get; set; }
	}
}
