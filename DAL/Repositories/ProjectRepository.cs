using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class ProjectRepository : Repository<Project>, IProjectRepository
	{
		public ProjectRepository(TaskDbContext context)
			: base (context)
		{ }

		public async Task<IEnumerable<Project>> GetAllProjectsWithDetailsAsync(bool trackChanges) =>
			await FindAll(trackChanges)
					.Include(p => p.Manager)
					.Include(p => p.Tasks)
					.OrderBy(p => p.EndDate)
					.ToListAsync();


		public async Task<Project> GetProjectByIdWithDetailsAsync(int id, bool trackChanges) =>
			await FindByCondition(p => p.Id == id, trackChanges)
					.Include(p => p.Manager)
					.Include(p => p.Tasks)
					.SingleOrDefaultAsync();
	}
}
