using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
	public class ProjectStatisticDto
	{
		public int ProjectId { get; set; }
		public string Title { get; set; }
		public float CompletionPercentage { get; set; }
		public IEnumerable<int> FinishedTasksIds { get; set; }
		public IEnumerable<int> UnfinishedTaksIds { get; set; }
	}
}
