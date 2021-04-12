using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class TaskRepository : Repository<Entities.Task>, ITaskRepository
	{
		public TaskRepository(TaskDbContext context)
			: base (context)
		{ }

		public async Task<IEnumerable<Entities.Task>> GetAllTasksWithDetailsAsync(bool trackChanges) =>
			await FindAll(trackChanges)
					.Include(t => t.Project)
					.Include(t => t.Status)
					.Include(t => t.Developer)
					.OrderBy(t => t.DeadLine)
					.ToListAsync();


		public async Task<Entities.Task> GetTaskByIdWithDetailsAsync(int id, bool trackChanges) =>
			await FindByCondition(t => t.Id == id, trackChanges)
					.Include(t => t.Project)
					.Include(t => t.Status)
					.Include(t => t.Developer)
					.SingleOrDefaultAsync();
	}
}
