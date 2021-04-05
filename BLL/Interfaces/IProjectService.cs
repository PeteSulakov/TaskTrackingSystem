using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IProjectService : ICrud<CreateProjectDto, ReadProjectDto, UpdateProjectDto>
	{
		Task<IEnumerable<ReadProjectDto>> GetProjectsByManagerIdAsync(string managerId);
		Task<ReadTaskDto> GetTaskInProjectAsync(int projectId, int taskId, string managerId);
	}
}
