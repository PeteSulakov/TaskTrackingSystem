using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface ITaskRepository : IRepository<Entities.Task>
	{
		Task<Entities.Task> GetTaskByIdWithDetailsAsync(int id, bool trackChanges);
		Task<IEnumerable<Entities.Task>>GetAllTasksWithDetailsAsync(bool trackChanges);

	}
}
