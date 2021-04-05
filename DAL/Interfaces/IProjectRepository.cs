using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IProjectRepository : IRepository<Project>
	{
		Task<IEnumerable<Project>> GetAllProjectsAsync(bool trackChanges);
		Task<Project> GetProjectByIdWithDetailsAsync(int id, bool trackChanges);
		Task<IEnumerable<Project>> GetAllProjectsWithDetailsAsync(bool trackChanges);
	}
}
