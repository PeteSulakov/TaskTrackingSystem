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
	public class StatusRepository : Repository<Status>, IStatusRepository
	{
		public StatusRepository(TaskDbContext context)
			: base(context)
		{ }

		public async Task<IEnumerable<Status>> GetAllStatusesWithDetailsAsync(bool trackChanges) =>
			await FindAll(trackChanges)
					.Include(s => s.Tasks)
					.ToListAsync();

		public async Task<Status> GetStatusByIdWithDetailsAsync(int id, bool trackChanges) =>
			await FindByCondition(p => p.Id == id, trackChanges)
					.Include(s => s.Tasks)
					.SingleOrDefaultAsync();
	}
}
