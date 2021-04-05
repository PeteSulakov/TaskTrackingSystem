using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface ITaskService : ICrud<CreateTaskDto, ReadTaskDto, UpdateTaskDto>
	{
		Task<IEnumerable<ReadTaskDto>> GetTasksByDeveloperIdAsync(string developerId);
		Task<ReadTaskDto> UpdateTaskStatus(int taskId, int statusId, string developerId);
	}
}
