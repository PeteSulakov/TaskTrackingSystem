using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IStatusRepository : IRepository<Status>
	{
		Task<Status> GetStatusByIdWithDetailsAsync(int id, bool trackChanges);
		Task<IEnumerable<Status>> GetAllStatusesWithDetailsAsync(bool trackChanges);
	}
}
