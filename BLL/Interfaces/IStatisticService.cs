using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IStatisticService
	{
		Task<ProjectStatisticDto> GetProjectStatisticAsync(int projectId, string managerId);
	}
}
